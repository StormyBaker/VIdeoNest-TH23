import { Button } from 'primereact/button';
import { useState } from 'react';
import { useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import { LoadingComponent } from '../../Components/LoadingComponent/LoadingComponent';
import { getNest } from '../../Service/NestService';
import {ReactComponent as TikTokIcon} from '../../Assets/tiktok.svg';

import './ViewNest.css'
import { Card } from 'primereact/card';

export function ViewNest() {
    const { nestGuid } = useParams();
    const [nest, setNest] = useState(false);

    useEffect(() => {
        (async() => {
            const response = await getNest(nestGuid);

            if (response.status === 200) {
                setNest(response.data);
            }
        })();
    }, []);

    if (!nest) {
        return <LoadingComponent/>
    } else {
        return (
            <div>
                <div className="grid">
                    <div className="col-6">
                        <h2 className='mb-0'>{nest.playlist.name}</h2>
                        <span>{nest.playlist.description}</span>
                    </div>
                    <div className="col-6">
                        <div className='flex justify-content-end'>
                            <Button label="Manage Nest"/>
                        </div>
                    </div>
                </div>
                <hr/>
                
                <div className='grid'>
                    {nest.videos.map(video => {
                        return (
                            <div className='col-6 sm:col-4 md:col-4 lg:col-3 xl:col-2'>
                                <Link to={`/app/nests/${nestGuid}/${video.guid}`}>
                                    <div className="nest-video-preview" style={{backgroundImage: `url('http://localhost:5250/video/thumbnail/${video.guid}')`}}>
                                        {
                                            video.sourcetype === 1 ?
                                            <TikTokIcon className='source-icon'/>
                                            :
                                            <i className="pi pi-youtube text-red-500 text-xl source-icon"></i>
                                        }                                    
                                        <span>{video.title}</span>
                                    </div>
                                </Link>
                            </div>
                        );
                    })}
                </div>
            </div>
        )
    }
}