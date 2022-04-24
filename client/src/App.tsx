import React, { useEffect } from 'react';
import './App.css';
import { Route, Switch } from 'react-router-dom';
import { Container } from '@material-ui/core';

import { userStore } from 'src/store/currentUser';

import { PrimaryAppBar } from './components/PrimaryAppBar';
import { MainMenu } from './components/MainMenu';
import { Footer } from './components/Footer';
import { Basket } from './components/Basket';
import { menuStore } from './store/currentMenu';

export const App: React.FC = () => {
  useEffect(() => {
    const getData = async () => {
      if (menuStore.pizzas.length === 0) {
        console.log(menuStore.pizzas.length);
        console.log(Date.now());
        console.log('started loading');
        await menuStore.loadData();
        await userStore.loadData();
        console.log('loaded');
        console.log(menuStore.pizzas.length);
      }
    };
    getData();
  }, []);

  return (
    <div>
      <div className={'mainContainer'}>
        <PrimaryAppBar />
        <Container>
          <Switch>
            <Route exact path="/" component={MainMenu} />
            <Route exact path="/Basket" component={Basket} />
          </Switch>
        </Container>
        <div className={'footerSpace'} />
      </div>
      <Footer />
    </div>
  );
};
