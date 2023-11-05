import React, { useRef, useState } from 'react';
import { Toast } from 'primereact/toast';

export const GlobalDataContext = React.createContext();

export const GlobalDataProvider = (props) => {
    const [user, setUser] = useState(null);
    const tst = useRef(null);
    const [removeMainPadding, setRemoveMainPadding] = useState(false);

    function toast(severity, summary, detail) {
        tst.current.show({severity, summary, detail});
    }

    const values = {
        user,
        setUser,
        toast,
        removeMainPadding,
        setRemoveMainPadding
    };

    return (
        <GlobalDataContext.Provider value={values}>
            <Toast ref={tst} />
            {props.children}
        </GlobalDataContext.Provider>
        );
};
