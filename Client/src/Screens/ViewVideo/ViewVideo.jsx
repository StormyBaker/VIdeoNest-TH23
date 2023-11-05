import { useEffect } from 'react';
import { useContext } from 'react';
import { useParams } from 'react-router-dom';
import { API_BASE_URL } from '../../App';
import { GlobalDataContext } from '../../Context/GlobalDataContext';

import './ViewVideo.css'

export function ViewVideo() {
    const { nestGuid, videoGuid } = useParams();
    const { setRemoveMainPadding } = useContext(GlobalDataContext);

    useEffect(() => {
        setRemoveMainPadding(true);

        return () => { setRemoveMainPadding(false)}
    }, [])

    return (
        <div className="flex justify-content-center">
            <video className="video-display" src={`${API_BASE_URL}/video/file/${videoGuid}`} controls={true}>
            </video>
        </div>
    )
}