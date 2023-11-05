import { InputText } from 'primereact/inputtext';
import { Menubar } from 'primereact/menubar';
import { Link, useNavigate } from 'react-router-dom';
import { NavbarProfile } from '../NavbarProfile/NavbarProfile';
import { VideoNestText } from '../VideoNestText/VideoNestText';

import './Navbar.css';

export function Navbar() {
    const navigate = useNavigate();

    const items = [
        {
            label: 'Nests',
            icon: 'pi pi-fw pi-list',
            command: () => {
                navigate('/app/nests')
            }
        }
    ];

    const start = <Link to="/app"><VideoNestText navbar height="40"/></Link>;
    const end = <NavbarProfile/>;

    return (
        <div className="card">
            <Menubar model={items} start={start} end={end} />
        </div>
    )
}