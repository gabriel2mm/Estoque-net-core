import React, { useEffect, useState } from 'react';
import ButtonComponent from '../../components/buttonComponent/index';
import { Alert } from 'antd';
import "./styles.css";

const SuccessPage = () => {
  const [order, setOrder] = useState(null);
  const queryString = window.location.search;

  useEffect(() => {

    async function search() {

      const urlParams = new URLSearchParams(queryString);
      const order = urlParams.get('pedido');
      setOrder(order);
    }

    search();
  }, [queryString]);

  return (
    <section id="order">
      <div className="title">
        <h1>Pedido Criado</h1>
        <div className="message">
          <Alert message={`Seu pedido Nº ${order} foi criado`} type="success" />
        </div>
        <div className="button">
          <ButtonComponent value="Continuar" link="/products" className="primary" type="button" />
        </div>
      </div>
    </section>
  );
}

export default SuccessPage;