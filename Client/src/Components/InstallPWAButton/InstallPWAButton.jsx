import { Button } from "primereact/button";
import { useEffect } from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

export function InstallPWAButton() {
    const [show, setShow] = useState(false);
    const [installPromptEvent, setInstallPromptEvent] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {

        function handler(e) {
            e.preventDefault();

            setShow(true);
            setInstallPromptEvent(e);
        }

        window.addEventListener("beforeinstallprompt", handler);

        return () => {
            window.removeEventListener("transitionend", handler)
        }
    }, []);

    function promptInstall() {
        if (!promptInstall) {
            return;
        }

        installPromptEvent.prompt();

        installPromptEvent.userChoice.then((choiceResult) => {
            if (choiceResult.outcome === 'accepted') {
                navigate('/app');
            }
        })
    }

    if (show) {
        return (
            <Button severity="success" label="Install VideoNest" className="mr-3 p-button-raised" onClick={promptInstall}/>
        )
    }
}