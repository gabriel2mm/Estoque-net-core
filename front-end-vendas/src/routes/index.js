import React from 'react';
import {Switch, Route} from 'react-router-dom';
import ProductsPage from '../pages/products/index';
import FavoritesPage from '../pages/favorites/index';
import CartPage from '../pages/cart/index';
import SearchPage from '../pages/search';
import PaymentPage from '../pages/payment';
import SuccessPage from '../pages/success';
import ErrorPage from '../pages/error';

export const Routes = () => {
    return (
      <Switch>
        <Route exact path="/" component={ProductsPage}/>
        <Route path="/products" component={ProductsPage}/>
        <Route path="/favorites" component={FavoritesPage}/>
        <Route path="/cart" component={CartPage}/>
        <Route path="/search" component={SearchPage}/>
        <Route path="/payment" component={PaymentPage}/>
        <Route path="/order-success" component={SuccessPage}/>
        <Route path="/order-error" component={ErrorPage}/>
        <Route path="/**" render={() => <span>Nenhum componente disponível</span>} />
      </Switch>
    )
}