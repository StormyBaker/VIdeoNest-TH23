import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { useContext } from "react";
import { useState } from "react";
import { GlobalDataContext } from "../../Context/GlobalDataContext";
import { requestDownload } from "../../Service/DownloadService";

export function VideoDownloadForm() {
    const [savingVideo, setSavingVideo] = useState(false);
    const { toast } = useContext(GlobalDataContext);

    async function doDownload(e) {
        e.preventDefault();
  
        const url = e.currentTarget["url"].value;

        setSavingVideo(true);
        toast("info", "Searching Video", "Your video is being downloaded, please wait.");
        await requestDownload(url);
        toast("success", "Video Nested", "The requested video has been added to your nest!");
        setSavingVideo(false);
    }

    return (
        <form onSubmit={doDownload}>
            <div className="p-inputgroup flex-1">
                <InputText name="url" placeholder="TikTok / YouTube Link" disabled={savingVideo}/>
                <Button label={savingVideo ? 'Saving' : 'Save Video'} disabled={savingVideo} loading={savingVideo} />
            </div>
        </form>
    )
}