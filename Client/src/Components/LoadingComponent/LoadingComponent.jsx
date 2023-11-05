import { ProgressSpinner } from "primereact/progressspinner";
import { VideoNestText } from "../VideoNestText/VideoNestText";

import './LoadingComponent.css'

export function LoadingComponent() {
    return (
        <div className="loading-component flex justify-content-center text-center align-items-center">
            <div>
                <VideoNestText/>
                
                <ProgressSpinner strokeWidth="5"/>
            </div>
        </div>
    )
}