import React, { useContext } from 'react';

import './App.css';
import { Outlet } from 'react-router-dom';
import { AuthenticationControl } from './Components/AuthenticationControl/AuthenticationControl';
import { GlobalDataContext } from './Context/GlobalDataContext';
import { LoadingScreen } from './Screens/LoadingScreen/LoadingScreen';
import { Navbar } from './Components/Navbar/Navbar';

function App() {
  const { user, removeMainPadding } = useContext(GlobalDataContext);

  var mainStyles = {

  }

  if (removeMainPadding) {
    mainStyles.padding = "0";
  }

  if (user) {
    return (
      <div>
        <Navbar/>
        <main style={mainStyles}>
          <Outlet/>
        </main>
      </div>
    )
  } else {
    return (
      <div>
        <AuthenticationControl/>
        <LoadingScreen/>
      </div>
    );
  }
}

export default App;

//export const API_BASE_URL = "http://localhost:5250";
export const API_BASE_URL = "https://videonest.us/api";
