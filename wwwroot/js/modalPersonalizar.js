function abrirModalPersonalizar(nome) {
    document.getElementById("tituloModalPersonalizar").innerText = nome;
    document.getElementById("qtdHamburguer").innerText = 1;
    document.getElementById("modalPersonalizar").style.display = "flex";

    document.querySelectorAll('#modalPersonalizar input[type=checkbox]').forEach(cb => cb.checked = false);
  }

  function fecharModalPersonalizar() {
    document.getElementById("modalPersonalizar").style.display = "none";
  }

  function alterarQtdExtra(tipo, valor) {
    if (tipo === 'hamburguer') {
      let span = document.getElementById('qtdHamburguer');
      let atual = parseInt(span.innerText);
      let novo = atual + valor;
      if (novo >= 0 && novo <= 3) span.innerText = novo;
    }
  }
  function adicionarItem(qnt,id){
    let precounit;
    let preco;
    let atual = parseInt(document.getElementById(id).innerText);
    if(precounit == null){
      precounit = parseFloat(document.getElementById(id+'preco').innerText.replace('R$', ''));
      precounit = precounit / atual;
    }
    // o ideal para evitar o erro de nan é usar o valor do banco depois no preco unitario
    atual += qnt;
    
    if(atual != 0){ preco = precounit * atual;}
    else { preco = 0;}
    document.getElementById(id+'preco').innerText = 'R$' + preco.toFixed(2);
    if(atual < 0) {atual = 0;}
    document.getElementById(id).innerHTML = atual;

  }