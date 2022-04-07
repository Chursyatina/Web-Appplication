import React from 'react';
import { Route, Switch } from 'react-router';
import './App.css';
import { BrowserRouter } from 'react-router-dom';

import { PrimaryAppBar } from './components/PrimaryAppBar';
import { MainMenu } from './components/MainMenu';
import { Footer } from './components/Footer';
import { Basket } from './components/Basket';

export const App: React.FC = () => (
  <div>
    <div className={'mainContainer'}>
      <PrimaryAppBar />
      <BrowserRouter>
        <Switch>
          <Route exact path="/" component={MainMenu} />
          <Route exact path="/Basket" component={Basket} />
        </Switch>
      </BrowserRouter>
      <div className={'footerSpace'} />
    </div>
    <Footer />
  </div>
);
