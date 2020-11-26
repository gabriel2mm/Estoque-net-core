import React from 'react';
import { Empty } from 'antd';
import CardComponent from '../cardComponent/index';
import './styles.css';

const CardListComponent = ({products}) => {

  function renderCardList() {
    if (!products|| products.length <= 0) {
      return (
        <Empty description="Não há produtos a serem exibidos :(" className="empty" />
      )
    } else {
      return products.map((p, index) => <CardComponent key={index} product={p} />);
    }
  }

  return (
    <>
      <div className="card_list">
        {renderCardList()}
      </div>
    </>
  );
}

export default CardListComponent;