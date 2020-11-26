import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import {CartContextProvider} from './context/productContext';
import {LayoutPage} from './pages/layout/index';
import {Routes} from './routes/index';

function App() {
    return (
        <CartContextProvider>
            <BrowserRouter>
                <LayoutPage>
                    <Routes />
                </LayoutPage>
            </BrowserRouter>
        </CartContextProvider>
    )
}

export default App;