import React, { useEffect, useState } from 'react';
import { Select } from 'antd';
import { useHistory } from 'react-router-dom';
import { useCartContext } from '../../context/productContext';
import { API } from '../../services/API';
import InputMask from 'react-input-mask';
import Button from '../../components/buttonComponent/index';
import axios from 'axios';
import "./styles.css";

function PaymentPage() {
  const { products, cartReset } = useCartContext();
  const [payment, setPayment] = useState({ nome: "", email: "", cpf: "", cep: "", endereco: "", bairro: "", numero: 0, cidade: "", estado: "", complemento: "", pagamento: "boleto", cartao: "", ccv: "", vencimento: "", parcelas: '1' });
  const history = useHistory();

  useEffect(() => {
    if (!products || products.length <= 0) {
      history.push('/cart');
    }
  }, [history, products]);

  function handleChangeText(e) {
    setPayment({ ...payment, [e.target.name]: e.target.value });
  }
  function handleOnChange(e, name) {
    setPayment({ ...payment, [name]: e });
  }

  function handleOnChangeCartao(e, name) {
    setPayment({ ...payment, parcelas: "1", cartao: "", vencimento: "", ccv: "", [name]: e });
  }

  async function handleBlurFindCEP(e) {
    const response = await axios.get(`http://viacep.com.br/ws/${e.target.value}/json/`);
    if (response) {
      setPayment({ ...payment, estado: response.data.uf, cidade: response.data.localidade, bairro: response.data.bairro, endereco: response.data.logradouro })
    }
  }

  function totalValue() {
    let total = 0;
    products.map(p => total += p.quantidade * p.productManagement.product.price);

    return total;
  }

  function onSubmit(e) {
    e.preventDefault();
    const location = { name: payment.endereco, CEP: payment.cep.replace(/\D/g, ''), Street: payment.endereco, District: payment.bairro, City: payment.cidade, State: payment.estado, Number: payment.numero, Complement: payment.complemento };
    const tmpPayment = { payment: payment.pagamento, card: payment.cartao, ccv: payment.ccv, dueDate: payment.vencimento, plots: payment.parcelas };
    const data = { OrderCode: "", name: payment.nome, email: payment.email, cpf: payment.cpf.replace(/\D/g, ''), Products: products, Location: location, Payment: tmpPayment };

    API.post('/api/Orders', { ...data })
      .then(resp => {
        window.location = `/order-success?pedido=${resp.data}`;
      })
      .catch(err => { history.push(`/order-error`); })
  }

  return (
    <form onSubmit={e => onSubmit(e)}>
      <div className="title-section">
        <h1>Informações do comprador</h1>
      </div>
      <div className="user">
        <div className="form-group">
          <label>Nome *:</label>
          <input type="text" name="nome" className="ant-input" value={payment.nome} placeholder="Digite seu nome completo" onChange={e => handleChangeText(e)} required />
        </div>
        <div className="form-group">
          <label>E-mail *:</label>
          <input type="email" name="email" className="ant-input" value={payment.email} placeholder="Digite seu email" onChange={e => handleChangeText(e)} required />
        </div>
        <div className="form-group">
          <label>CPF *:</label>
          <InputMask type="text" name="cpf" mask="999.999.999-99" maskChar=" " className="antd-input input" minLength="14" value={payment.cpf} placeholder="Digite seu CPF" onChange={e => handleChangeText(e)} required />
        </div>
      </div>

      <div className="title-section">
        <h1>Endereço para entrega</h1>
      </div>
      <div className="informacoesEntrega">
        <div className="form-group-3">
          <label>CEP *:</label>
          <InputMask type="text" onBlur={e => handleBlurFindCEP(e)} name="cep" mask="99999-999" className="antd-input input" value={payment.cep} placeholder="Digite seu CEP" onChange={e => handleChangeText(e)} required />
        </div>
        <div className="form-group-3">
          <label>Endereço *:</label>
          <input type="text" name="endereco" className="ant-input input" placeholder="Digite seu endereço" value={payment.endereco} onChange={e => handleChangeText(e)} required />
        </div>
        <div className="form-group-3">
          <label>Número *:</label>
          <input type="number" name="numero" className="antd-input input" placeholder="Digite o número" value={payment.numero} onChange={e => handleChangeText(e)} required />
        </div>
        <div className="form-group-3">
          <label>Bairro *:</label>
          <input type="text" name="bairro" className="antd-input input" placeholder="Digite o bairro" value={payment.bairro} onChange={e => handleChangeText(e)} required />
        </div>
        <div className="form-group-3">
          <label>Cidade *:</label>
          <input type="text" name="cidade" className="antd-input input" placeholder="Digite a cidade" value={payment.cidade} onChange={e => handleChangeText(e)} required />
        </div>
        <div className="form-group-3">
          <label>Estado *:</label>
          <Select value={payment.estado} onChange={e => handleOnChange(e, 'estado')} name="estado" id="estado" className="antd-input input" required>
            <Select.Option value="AC">Acre</Select.Option>
            <Select.Option value="AL">Alagoas</Select.Option>
            <Select.Option value="AP">Amapá</Select.Option>
            <Select.Option value="AM">Amazonas</Select.Option>
            <Select.Option value="BA">Bahia</Select.Option>
            <Select.Option value="CE">Ceará</Select.Option>
            <Select.Option value="DF">Distrito Federal</Select.Option>
            <Select.Option value="ES">Espírito Santo</Select.Option>
            <Select.Option value="GO">Goiás</Select.Option>
            <Select.Option value="MA">Maranhão</Select.Option>
            <Select.Option value="MT">Mato Grosso</Select.Option>
            <Select.Option value="MS">Mato Grosso do Sul</Select.Option>
            <Select.Option value="MG">Minas Gerais</Select.Option>
            <Select.Option value="PA">Pará</Select.Option>
            <Select.Option value="PB">Paraíba</Select.Option>
            <Select.Option value="PR">Paraná</Select.Option>
            <Select.Option value="PE">Pernambuco</Select.Option>
            <Select.Option value="PI">Piauí</Select.Option>
            <Select.Option value="RR">Roraima</Select.Option>
            <Select.Option value="RO">Rondônia</Select.Option>
            <Select.Option value="RJ">Rio de Janeiro</Select.Option>
            <Select.Option value="RN">Rio Grande do Norte</Select.Option>
            <Select.Option value="RS">Rio Grande do Sul</Select.Option>
            <Select.Option value="SC">Santa Catarina</Select.Option>
            <Select.Option value="SP">São Paulo</Select.Option>
            <Select.Option value="SE">Sergipe</Select.Option>
            <Select.Option value="TO">Tocantins</Select.Option>
          </Select>
        </div>
        <div className="form-group-3">
          <label>Complemento:</label>
          <input type="text" name="complemento" className="antd-input input" placeholder="Digite o complemento. Ex: bloco 1 apto 2 (opcional)" value={payment.complemento} onChange={e => handleChangeText(e)} />
        </div>
      </div>

      <div className="title-section">
        <h1>Informações de pagamento</h1>
      </div>
      <div className="informacoesPagamento">
        <div className="form-group">
          <label>Tipo de pagamento *:</label>
          <Select value={payment.pagamento} onChange={e => handleOnChangeCartao(e, 'pagamento')} name="pagamento" id="pagamento" className="antd-input input" required>
            <Select.Option value="boleto">Boleto</Select.Option>
            <Select.Option value="credito">Cartão Crédito</Select.Option>
          </Select>
        </div>

        {payment.pagamento === "boleto" ? (<div className="boleto">
          <p> 1x de {totalValue().toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })}</p>
        </div>) : (<div className="credito">
          <div className="form-group-3">
            <label>Número Cartão de crédito *:</label>
            <input type="text" name="cartao" className="antd-input input" placeholder="Digite o número do cartão" value={payment.cartao} onChange={e => handleChangeText(e)} required />
          </div>
          <div className="form-group-3">
            <label>Vencimento:</label>
            <InputMask type="text" name="vencimento" mask="99/99" className="antd-input input" value={payment.vencimento} placeholder="Digite o vencimento" onChange={e => handleChangeText(e)} required />
          </div>
          <div className="form-group-3">
            <label>CCV</label>
            <InputMask type="text" name="ccv" mask="999" className="antd-input input" value={payment.ccv} placeholder="Digite o CCV" onChange={e => handleChangeText(e)} required />
          </div>
          <div className="form-group-3">
            <label>Número de parcelas</label>
            <Select value={payment.parcelas} onChange={e => handleOnChange(e, 'parcelas')} name="parcelas" id="parcelas" className="antd-input input" required>
              <Select.Option value="1">1</Select.Option>
              <Select.Option value="2">2</Select.Option>
              <Select.Option value="3">3</Select.Option>
              <Select.Option value="4">4</Select.Option>
              <Select.Option value="5">5</Select.Option>
              <Select.Option value="6">6</Select.Option>
              <Select.Option value="7">7</Select.Option>
              <Select.Option value="8">8</Select.Option>
              <Select.Option value="9">9</Select.Option>
              <Select.Option value="10">10</Select.Option>
              <Select.Option value="11">11</Select.Option>
              <Select.Option value="12">12</Select.Option>
            </Select>
          </div>
          <div className="form-group">
            <p> {payment.parcelas} parcelas de {(totalValue() / payment.parcelas).toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })}</p>
          </div>
        </div>)}
      </div>

      <div className="form-group">
        <Button type="submit" value="Finalizar" className="primary" />
      </div>
    </form>
  );
}

export default PaymentPage;