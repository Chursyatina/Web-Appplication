import React, { useEffect } from 'react';
import './App.css';
import { Route, Switch } from 'react-router-dom';
import { Container } from '@material-ui/core';

import { userStore } from 'src/store/currentUser';
import { PrimaryAppBar } from 'src/components/PrimaryAppBar';
import { MainMenu } from 'src/components/MainMenu';
import { Footer } from 'src/components/Footer';
import { Basket } from 'src/components/Basket';
import { IngredientsCatalog } from 'src/components/IngredientCatalog';
import { menuStore } from 'src/store/currentMenu';
import { AdditionalIngredientsCatalog } from 'src/components/AdditionalIngredientsCatalog';

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
            <Route exact path="/IngredientsCatalog" component={IngredientsCatalog} />
            <Route exact path="/AdditionalIngredientsCatalog" component={AdditionalIngredientsCatalog} />
          </Switch>
        </Container>
        <div className={'footerSpace'} />
      </div>
      <Footer />
    </div>
  );
};
