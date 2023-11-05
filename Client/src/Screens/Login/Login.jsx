import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { InputText } from "primereact/inputtext";
import { Link, useNavigate } from "react-router-dom";
import "./Login.css";

import { VideoNestText } from "../../Components/VideoNestText/VideoNestText";
import { useContext } from "react";
import { GlobalDataContext } from "../../Context/GlobalDataContext";
import { login } from "../../Service/AuthenticationService";

export function Login() {
    const { toast } = useContext(GlobalDataContext);
    const navigate = useNavigate();
  
    async function doLogin(e) {
      e.preventDefault();
  
      const email = e.currentTarget["email"].value;
      const password = e.currentTarget["password"].value;

      const result = await login(email, password);
  
      if (result.status === 401) {
        return toast("error", "Invalid Credentials", `The credentials you entered are not associated with any account.`);
      }
  
      if (result.status === 200) {
        navigate("/app");
      }
    }

  return (
    <div className="login-main-screen flex align-items-center justify-content-center">
      <div className="surface-card p-4 shadow-2 border-round w-full lg:w-6">
        <div className="mb-5">
          <div className="login-header">
            <VideoNestText tagline />
          </div>
        </div>
        <div className="text-center mb-5">
          <div className="text-900 text-3xl font-medium mb-3">Welcome Back</div>
          <span className="text-600 font-medium line-height-3">
            Don't have an account?
          </span>
          <Link
            to={`/register`}
            className="font-medium no-underline ml-2 text-blue-500 cursor-pointer"
          >
            Create one today!
          </Link>
        </div>

        <form onSubmit={doLogin}>
          <label htmlFor="email" className="block text-900 font-medium mb-2">
            Email
          </label>
          <InputText
            name="email"
            type="text"
            placeholder="Email address"
            className="w-full mb-3"
          />

          <label htmlFor="password" className="block text-900 font-medium mb-2">
            Password
          </label>
          <InputText
            name="password"
            type="password"
            placeholder="Password"
            className="w-full mb-3"
          />

          <Button label="Sign In" icon="pi pi-user" className="w-full" />
        </form>
      </div>
    </div>
  );
}
