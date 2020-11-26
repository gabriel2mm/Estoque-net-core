import React from 'react';
import ButtonComponent from '../../components/buttonComponent/index';
import { Alert } from 'antd';
import "./styles.css";

const ErrorPage = () => {
  return (
    <section id="order">
      <div className="title">
        <h1>Erro ao criar seu pedido</h1>
        <div className="message">
          <Alert message={`Não foi possível criar seu pedido. Tente novamente`} type="error" />
        </div>
        <div className="button">
          <ButtonComponent value="Tentar Novamente" link="/cart" className="primary" type="button" />
        </div>
      </div>
    </section>
  );
}

export default ErrorPage;