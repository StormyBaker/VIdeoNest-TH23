import { useContext } from "react";
import { useEffect } from "react";
import { useRef } from "react";
import { Messages } from 'primereact/messages';
import { GlobalDataContext } from "../../Context/GlobalDataContext";
import { Link } from "react-router-dom";

export function NewUserMessages() {
    const msgs = useRef(null);
    const { user } = useContext(GlobalDataContext);
    
    useEffect(() => {
        let messagesToShow = []
        
        if (!user.profile.username) {
            messagesToShow.push({ sticky: true, severity: 'info', summary: 'Profile', 
            detail: (
                <span>Select a display name for your profile! <Link to="/app/profile">Customize Profile</Link></span>
            )});
        }

        if (!user.profile.biography) {
            messagesToShow.push({ sticky: true, severity: 'info', summary: 'Profile', 
            detail: (
                <span>Write a biography for your profile! <Link to="/app/profile">Customize Profile</Link></span>
            )});
        }

        if (!user.profile.image) {
            messagesToShow.push({ sticky: true, severity: 'info', summary: 'Profile', 
            detail: (
                <span>Upload a profile picture! <Link to="/app/profile">Customize Profile</Link></span>
            )});
        }

        if (msgs.current) {
            msgs.current.clear();
            msgs.current.show(messagesToShow);
        }
    }, []);

    return (
        <div className="card">
            <Messages ref={msgs} />
        </div>
    )
}