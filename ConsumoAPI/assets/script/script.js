
/*
const getCEP = async () => {
    const cep = document.getElementById("cep").value; 
    
    if(cep.len !== 8){
        alert("CEP inválido. Digite um CEP com 8 digítos.");
        return;
    }

    const response = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
    return response.json;
}
*/


function buscarCEP() {
    const cep = document.getElementById("cep").value; 
    const retorno = document.getElementById("retorno").style;
    const botaoCadastro = document.getElementById("btn-cadastrar").style;
    const classeBotao = document.querySelectorAll(".btn");

    if(cep.length !== 8){
        alert("CEP inválido. Digite um CEP com 8 digítos.");
        return;
    }

    fetch(`https://viacep.com.br/ws/${cep}/json/`).then(response => response.json()).then(data => {
        if(data.erro){
            alert("CEP não encontrado.");
            return;
        }
        document.getElementById("logradouro").textContent = data.logradouro || "";

        document.getElementById("bairro").textContent = data.bairro || ""; 
        
        document.getElementById("cidade").textContent = data.localidade || "";

        document.getElementById("estado").textContent = data.uf || "";

    }).catch(error => {
        alert("Erro ao buscar CEP");
    })

    retorno.display = "block";
    botaoCadastro.display = "block";
    classeBotao.forEach((elemento) =>{
        elemento.style.fontSize = "1.6em";
        elemento.style.padding = "0";
        elemento.style.width = "40%";
        elemento.style.height = "80px";
    });
    
}

