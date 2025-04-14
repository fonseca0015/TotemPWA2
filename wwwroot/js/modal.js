function abrirModal(nome, preco, descricao, imgSrc) {
    document.getElementById('modalTitulo').innerText = nome;
    document.getElementById('modalPreco').innerText = preco;
    document.getElementById('modalDescricao').innerText = descricao;
    document.getElementById('modalImg').src = imgSrc;
    document.getElementById('modalQtd').innerText = 1;
    document.getElementById('modal').style.display = 'flex';
}

function fecharModal() {
    document.getElementById('modal').style.display = 'none';
}

function alterarQtd(valor) {
    const span = document.getElementById('modalQtd');
    const atual = parseInt(span.innerText);
    const nova = atual + valor;
    if (nova >= 1) span.innerText = nova;
}