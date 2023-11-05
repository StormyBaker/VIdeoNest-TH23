import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';

import 'primeicons/primeicons.css';
import "primereact/resources/primereact.min.css"; 
import "./Assets/theme.css";
import 'primeflex/primeflex.css';

import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { Login } from './Screens/Login/Login';
import { Register } from './Screens/Register/Register';
import { GlobalDataProvider } from './Context/GlobalDataContext';
import { Home } from './Screens/Home/Home';
import { SplashScreen } from './Screens/SplashScreen/SplashScreen';
import { MyProfile } from './Screens/MyProfile/MyProfile';
import { Nests } from './Screens/Nests/Nests';
import { ViewNest } from './Screens/ViewNest/ViewNest';
import { ViewVideo } from './Screens/ViewVideo/ViewVideo';

const router = createBrowserRouter([
  {
    path: "/",
    element: <SplashScreen/>
  },
  {
    path: "/app",
    element: <App/>,
    children: [
      {
        path: "/app",
        element: <Home/>
      },
      {
        path: "/app/profile",
        element: <MyProfile/>
      },
      {
        path: "/app/nests",
        element: <Nests/>
      },
      {
        path: "/app/nests/:nestGuid",
        element: <ViewNest/>
      },
      {
        path: "/app/nests/:nestGuid/:videoGuid",
        element: <ViewVideo/>
      }
    ]
  },
  {
    path: "/login",
    element: <Login/>
  },
  {
    path: "/register",
    element: <Register/>
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <GlobalDataProvider>
      <RouterProvider router={router} />
    </GlobalDataProvider>
  </React.StrictMode>
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
