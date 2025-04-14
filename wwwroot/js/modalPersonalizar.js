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