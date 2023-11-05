import { Button } from "primereact/button";
import { useContext } from "react";
import { Link } from "react-router-dom";
import VideoHero from '../../Assets/VideoHero.mp4';
import { AuthenticationControl } from "../../Components/AuthenticationControl/AuthenticationControl";
import { InstallPWAButton } from "../../Components/InstallPWAButton/InstallPWAButton";
import { VideoNestText } from "../../Components/VideoNestText/VideoNestText";
import { GlobalDataContext } from "../../Context/GlobalDataContext";
import './SplashScreen.css';

export function SplashScreen() {
    const { user } = useContext(GlobalDataContext);

    return (
        <div className="splash-screen-main">
            <AuthenticationControl noredirect/>
            <div className="grid grid-nogutter text-800 splash-screen-content">
                <div className="col-12 md:col-6 p-6 text-center md:text-left flex align-items-center splash-content">
                    <section>
                        <VideoNestText left/>
                        <span className="block text-3xl md:text-6xl font-bold mb-1">Create a personal archive</span>
                        <div className="text-3xl md:text-6xl text-primary font-bold mb-3">for the content you don't want to lose</div>
                        <p className="mt-0 mb-4 text-700 line-height-3">A Tiger Hacks 2023 entry - Archive TikToks and YouTube Shorts so you never lose them!</p>

                        <Link to={`/app`}>
                            <Button label={user ? 'Go to App' : 'Login / Register'} type="button" className="mr-3 p-button-raised" />
                        </Link>
                        <InstallPWAButton/>
                    </section>
                </div>
                <div className="col-12 md:col-6 overflow-hidden">
                    <video src={VideoHero} alt="hero-1" muted={true} autoPlay={true} loop={true} controls={false} className="splash-video md:ml-auto block md:h-full" />
                </div>
            </div>
        </div>
    )
}