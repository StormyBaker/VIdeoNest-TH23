import { useEffect } from "react";
import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { GlobalDataContext } from "../../Context/GlobalDataContext";
import { getUserInformation } from "../../Service/UserService";

export function AuthenticationControl(props) {
    const { user, setUser } = useContext(GlobalDataContext);
    const navigate = useNavigate();

    useEffect(()=>{
        (async() => {
            if (user) return;

            var userInformation = await getUserInformation();

            if (userInformation.status !== 200) {
                if (props.noredirect) return;
                
                navigate('/login');
                return;
            }

            setUser(userInformation.data);
        })();
    }, []);
}