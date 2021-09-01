import React from 'react';
import { Route, Switch } from 'react-router';
import './App.css';
import { BrowserRouter } from 'react-router-dom';

import { PrimaryAppBar } from './components/PrimaryAppBar';
import { MainMenu } from './components/MainMenu';
import { Footer } from './components/Footer';

export const App: React.FC = () => (
  <div>
    <div className={'mainContainer'}>
      <PrimaryAppBar />
      <BrowserRouter>
        <Switch>
          <Route exact path="/" component={MainMenu} />
        </Switch>
      </BrowserRouter>
      <div className={'footerSpace'} />
    </div>
    <Footer />
  </div>
);
