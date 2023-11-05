import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { Link, useNavigate } from "react-router-dom";
import "./Register.css";

import { VideoNestText } from "../../Components/VideoNestText/VideoNestText";
import { useContext } from "react";
import { GlobalDataContext } from "../../Context/GlobalDataContext";
import { register } from "../../Service/AuthenticationService";

export function Register() {
  const { toast } = useContext(GlobalDataContext);
  const navigate = useNavigate();

  async function doRegister(e) {
    e.preventDefault();

    const email = e.currentTarget["email"].value;
    const password = e.currentTarget["password"].value;
    const confirmPassword = e.currentTarget["confirmpassword"].value;

    if (password !== confirmPassword) {
      return toast("error", "Mismatched Passwords", "Your passwords do not match.");
    }

    const result = await register(email, password);

    if (result.status === 409) {
      return toast("error", "Already Registered", `An account already exists with the email ${email}`);
    }

    if (result.status === 200) {
      navigate("/login");
      return toast("success", "Account Registered", "Your account has been created!");
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
          <div className="text-900 text-3xl font-medium mb-3">Sign Up</div>
          <span className="text-600 font-medium line-height-3">
            Already have an account?
          </span>
          <Link
            to={`/login`}
            className="font-medium no-underline ml-2 text-blue-500 cursor-pointer"
          >
            Login!
          </Link>
        </div>

        <form onSubmit={doRegister}>
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

          <label htmlFor="password" className="block text-900 font-medium mb-2">
            Confirm Password
          </label>
          <InputText
            name="confirmpassword"
            type="password"
            placeholder="Confirm Password"
            className="w-full mb-3"
          />

          <Button label="Sign Up" icon="pi pi-user" className="w-full" />
        </form>
      </div>
    </div>
  );
}
