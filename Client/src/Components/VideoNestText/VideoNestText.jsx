import './VideoNestText.css';
import VideoNestLogo from "../../Assets/VideoNestLogo.png";

export function VideoNestText(props) {
    return (
        <span className={`videonest-text ${props.left ? 'left' : ''} ${props.right ? 'right' : ''} ${props.navbar ? 'navbar' : ''}`}>
            <img
                src={VideoNestLogo}
                alt="VideoNest logo, a nest with three eggs with play buttons"
                height={props.height ?? 100}
                />
            <div>
                <p className={`${props.navbar ? 'navbar' : ''}`}><span>V</span>ideo<span>N</span>est</p>
                {(props.tagline) ? 
                <div className="tagline">Keep your eggs safe in one basket.</div>
                : ''}
            </div>
        </span>
    )
}