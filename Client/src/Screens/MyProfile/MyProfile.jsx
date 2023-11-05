import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { InputTextarea } from 'primereact/inputtextarea';
import { Card } from 'primereact/card';
import { useState } from "react";

import './MyProfile.css';
import { Avatar } from "primereact/avatar";
import PlaceholderAvatar from '../../Assets/PlaceholderProfilePicture.webp';
import { updateUserInformation } from "../../Service/UserService";
import { useContext } from "react";
import { GlobalDataContext } from "../../Context/GlobalDataContext";
import { API_BASE_URL } from "../../App";

export function MyProfile() {
    const [edit, setEdit] = useState(false);
    const [selectedImage, setSelectedImage] = useState(null);
    const { user, setUser, toast } = useContext(GlobalDataContext);

    async function doProfileUpdate(e) {
        e.preventDefault();

        var dataToSend = new FormData();

        dataToSend.append("Username", e.currentTarget["Username"].value)
        dataToSend.append("Biography", e.currentTarget["Biography"].value)
        dataToSend.append("ProfilePicture", e.currentTarget["ProfilePicture"].files[0])

        const result = await updateUserInformation(dataToSend);

        if (result.status === 200) {
            var newUserObject = JSON.parse(JSON.stringify(user));
            newUserObject.profile = result.data.newUser;

            setUser(newUserObject);

            toast('success', 'Updated', 'Profile updated successfully!');
            setEdit(false);
        }
    }

    function openFileSelection() {
        if (!edit) return;
        document.getElementById('profile-upload-element').click();
    }

    const handleImageChange = (event) => {
        const file = event.target.files[0]; // Get the first selected file
        if (file) {
          setSelectedImage(URL.createObjectURL(file)); // Create a URL for the selected image
        }
      };

      var imageToUse = PlaceholderAvatar;

      if (user.profile?.image) {
          imageToUse = `${API_BASE_URL}/user/profilepicture/${user.profile.image}`;
      }

    return (
        <div className="grid profile-edit-main">
            <div className="col-12 lg:col-4">
                <Card title={edit ? 'Updating User Profile' : 'User Profile'}>
                    <form onSubmit={doProfileUpdate}>
                        <div className={`profile-picture flex align-items-center mb-3 gap-3 ${edit ? 'editable' : ''}`} onClick={openFileSelection}>
                            <Avatar image={selectedImage ?? imageToUse} size="xlarge" shape="circle" />
                            <span><b>Profile Picture</b></span>
                        </div>
                        <input id="profile-upload-element" className="hidden-file-input" type="file" accept="image/*" name="ProfilePicture" onChange={handleImageChange}/>
                        <div className="flex flex-column gap-2 mb-3">
                            <label htmlFor="username">Display Name</label>
                            <InputText id="username" name="Username" disabled={!edit} defaultValue={user?.profile?.username} />
                        </div>
                        <div className="flex flex-column gap-2 mb-3">
                            <label htmlFor="biography">Biography</label>
                            <InputTextarea id="biography" name="Biography" disabled={!edit} defaultValue={user?.profile?.biography}/>
                        </div>
                        {
                            !edit ?
                            <Button label="Edit" onClick={() => {setEdit(true)}}/>
                            :
                            <div>
                                <Button label="Update" className="mr-3"/>
                                <Button severity="warning" label="Cancel" onClick={() => {setEdit(false)}}/>
                            </div>
                        }
                    </form>   
                </Card>
            </div>
        </div>
    )
}