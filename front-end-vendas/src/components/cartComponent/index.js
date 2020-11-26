import React, { useState } from 'react';
import { Empty } from 'antd';
import { PlusOutlined, LineOutlined } from '@ant-design/icons';
import { useCartContext } from '../../context/productContext';
import {API} from '../../services/API';
import "./styles.css";

const CartComponent = () => {
  const { products, addProduct, removeProduct } = useCartContext();
  const [title , setTitle] = useState("Reservar");

  function handleAddProduct(product){
    addProduct(product)
  }
  function handleRemoveProduct(product){
    removeProduct(product)
  }

  function currencyString(product){
    const value =product.quantidade * product.productManagement.product.price;
    return value.toLocaleString('pt-br',{style: 'currency', currency: 'BRL'});
  }

  function handleReservation(p){
    const quantidade = p.quantidade;
    const id = p.productManagement.id;

    if(title === "Reservar" || title === "Cancelado"){
      API.post('/api/Reservations', { id, quantidade})
      .then(p => setTitle("Reservado"))
      .catch(err => {
        console.log(err);
        setTitle("Não foi possível reservar produto");
      })
    }else{
      API.delete('/api/Reservations',  { data: {id} })
      .then(p => setTitle("Cancelado"))
      .catch(err => {
        console.log(err);
        setTitle("Não foi possível cancelar reserva do produto");
      })
    }
  }

  function renderProducts() {
    if (products && products.length > 0) {
      return products.map((p, index) =>
        (
          <tr key={index}>
            <td className="ant-table-row ant-table-row-level-0"><img className="img" src={`https://imagensshowcase.s3-sa-east-1.amazonaws.com/imagens/${p.image}`} alt={p.productManagement.product.name} /></td>
            <td className="ant-table-row ant-table-row-level-0 title">{p.productManagement.product.name}</td>
            <td className="ant-table-row ant-table-row-level-0 title">
              {p.productManagement.amount > 1 ? (<button className="aumentar" onClick={e => handleAddProduct(p)}><PlusOutlined/></button>) : (null)}
              <span>{p.quantidade}</span>
              <button className="remover" onClick={e => handleRemoveProduct(p)}><LineOutlined/></button>
            </td>
            <td className="ant-table-row ant-table-row-level-0 title">{currencyString(p)}</td>
            <td><a href="#!" style={{color: 'blue'}} onClick={e => handleReservation(p)}>{title}</a></td>
          </tr>
        )
      )
    }

    return <tr><td className="ant-table-row ant-table-row-level-0" colSpan={4}><div className="mensagem"><Empty description="nenhum produto no carrinho :(" /></div></td></tr>
  }

  return (
    <>
      <table className="table">
        <thead className="ant-table-thead">
          <tr>
            <th>Imagem</th>
            <th>Produto</th>
            <th>Quantidade</th>
            <th>Subtotal</th>
            <th>Reservar</th>
          </tr>
        </thead>

        <tbody className="ant-table-tbody">
           {renderProducts()}
        </tbody>
      </table>
    </>
  )
}

export default CartComponent;

