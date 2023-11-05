import { StatCard } from "../StatCard/StatCard";
import {ReactComponent as VideoVectorNestIcon} from '../../Assets/VideoNestVector.svg';
import {ReactComponent as TikTokIcon} from '../../Assets/tiktok.svg';
import { useEffect } from "react";
import { getNestCount } from "../../Service/NestService";
import { useState } from "react";
import { getVideoCounts } from "../../Service/VideoService";

export function DashStats() {
    const [nestCount, setNestCount] = useState('Loading');
    const [youtubeCount, setYoutubeCount] = useState('Loading');
    const [tiktokCount, setTiktokCount] = useState('Loading');

    useEffect(() => {
        (async() => {
            var result = await getNestCount();
            var videoCounts = await getVideoCounts();

            if (result.status === 200) {
                setNestCount(result.data.count);
            }

            if (videoCounts.status === 200) {
                setYoutubeCount(videoCounts.data.youtube);
                setTiktokCount(videoCounts.data.tiktok);
            }
        })();
    }, []);

    return (
        <div className="grid">
            <div className="col-12 md:col-6 lg:col-4">
                <StatCard title="Nests" description={`${nestCount} nest${nestCount === 1 ? '' : 's'}`} icon={<VideoVectorNestIcon/>}/>
            </div>
            <div className="col-12 md:col-6 lg:col-4">
                <StatCard title="YouTube Shorts" description={`${youtubeCount} short${youtubeCount === 1 ? '' : 's'}`}
                icon={<i className="pi pi-youtube text-red-500 text-xl"></i>}/>
            </div>
            <div className="col-12 md:col-6 lg:col-4">
                <StatCard title="TikToks" description={`${tiktokCount} Tiktok${tiktokCount === 1 ? '' : 's'}`} icon={<TikTokIcon/>}/>
            </div>
        </div>   
    )
}