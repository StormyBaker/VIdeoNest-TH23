import { useContext } from "react"
import { DashStats } from "../../Components/DashStats/DashStats";
import { NewUserMessages } from "../../Components/NewUserMessages/NewUserMessages";
import { VideoDownloadForm } from "../../Components/VideoDownloadForm/VideoDownloadForm";
import { GlobalDataContext } from "../../Context/GlobalDataContext"
import { Card } from 'primereact/card';
import { Link } from "react-router-dom";

export function Home() {
    const { user } = useContext(GlobalDataContext);

    return (
        <div>
            <NewUserMessages />
            
            <DashStats />

            <div className="grid mt-0">
                <div className="col-12 md:col-6">
                    <Card 
                        title={<span>Hello, {user.profile.username ? user.profile.username : <Link to="/app/profile">you can customize your profile!</Link>}</span>}>
                        <p className="m-0">
                            VideoNest is a TigerHacks 2023 project.
                        </p>
                        <p>VideoNest may be used to</p>
                        <ul>
                            <li>Archive TikTok or YouTube videos.</li>
                            <li>Easily create nests/playlists of short-form content.</li>
                            <li>Share your interests with your friends.</li>
                        </ul>
                    </Card>
                </div>
                <div className="col-12 md:col-6">
                    <Card className="minheight-100" title="Save Content" footer={<VideoDownloadForm/>}>
                        <p className="m-0">
                            Insert a TikTok or YouTube link to save into your main nest! 
                        </p>
                    </Card>
                </div>

            </div>
        </div>
    )
}