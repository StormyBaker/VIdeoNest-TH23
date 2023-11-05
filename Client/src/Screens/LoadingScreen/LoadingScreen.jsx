import { ProgressSpinner } from "primereact/progressspinner";
import React from "react";
import { VideoNestText } from "../../Components/VideoNestText/VideoNestText";


import './LoadingScreen.css';

export function LoadingScreen() {
    return (
        <div class="loading-screen">
            <div>
                <VideoNestText/>
                
                <ProgressSpinner strokeWidth="5"/>
            </div>
        </div>
    )
}