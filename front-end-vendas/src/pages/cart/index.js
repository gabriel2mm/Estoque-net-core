import React from 'react';
import CartComponent from '../../components/cartComponent/index';
import ButtonComponent from '../../components/buttonComponent/index';
import { useCartContext } from '../../context/productContext';
import "./styles.css";

const CartPage = () => {

  const { products } = useCartContext();

  function totalValue() {
    let total = 0;
    products.map(p => total += p.quantidade * p.productManagement.product.price);

    return total.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });
  }

  return (
    <section id="cart">
      <div className="title">
        <h1>Meu Carrinho</h1>
      </div>
      <CartComponent />
      <div className="info">
        <div className="valorTotal">
          <span>Total : <b>{totalValue()}</b></span>
        </div>
        <div className="actions">
          <ButtonComponent value="Voltar" link='/products' className="danger" />
          {products.length > 0 ? ( <ButtonComponent value="Continuar" link='/payment' className="primary" />) : ( null )}
        </div>
      </div>
    </section>
  )
}

export default CartPage;