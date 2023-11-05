import { Avatar } from 'primereact/avatar';
import { useContext, useRef } from 'react';
import { API_BASE_URL } from '../../App';
import PlaceholderAvatar from '../../Assets/PlaceholderProfilePicture.webp';
import { GlobalDataContext } from '../../Context/GlobalDataContext';
import { OverlayPanel } from 'primereact/overlaypanel';
import { useNavigate } from 'react-router-dom';
import { Menu } from 'primereact/menu';

import './NavbarProfile.css'

export function NavbarProfile() {
    const { user } = useContext(GlobalDataContext);
    const op = useRef(null);
    const navigate = useNavigate();

    var imageToUse = PlaceholderAvatar;

    if (user.profile?.image) {
        imageToUse = `${API_BASE_URL}/user/profilepicture/${user.profile.image}`;
    }

    let profileItems = [
        {
            label: user.profile.username,
            items: [
                {
                    label: 'My Profile', 
                    icon: 'pi pi-fw pi-plus', 
                    command: () => {
                        navigate('/app/profile')
                    }
                }
            ]
        }
    ];

    return (
        <div>
            <Avatar image={`${imageToUse}`} size="large" shape="circle" onClick={(e) => op.current.toggle(e)} />
            <OverlayPanel className='profile-overlay' ref={op}>
                <Menu model={profileItems} />
            </OverlayPanel>
        </div>
    )
}