function buscarCEP() {
    
    const retorno = document.getElementById("retorno").style;
    const botaoCadastro = document.getElementById("btn-cadastrar").style;
    const classeBotao = document.querySelectorAll(".btn");
    const cep = document.getElementById("cep").value; 

    if(cep.length !== 8){
        alert("CEP inválido. Digite um CEP com 8 digítos.");
        return;
    }

    fetch(`https://localhost:7192/api/Endereco/EnderecoViaCep?cep=${cep}`).then(response => response.json()).then(data => {
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

function cadastrarEndereco() {

    const cep = document.getElementById("cep").value; 
    const complemento = document.getElementById("complemento").value;
    const numero = document.getElementById("numero").value;

    fetch(`https://localhost:7192/api/Endereco?cep=${cep}&complemento=${complemento}&numero=${numero}`
        , {method: "POST",
            headers: {
              'Content-Type': 'application/json'
            }}
    ).then(response => {
        if(!response.ok) {
            console.log("Erro na requisição:", response.statusText);
            throw new Error("Erro na requisição");
        }

        alert("Endereço cadastrado com sucesso!");
    });
}

/*
 headers = {
            "Content-Type": "application/json"
        }, body = JSON.stringify({
            cep: cep,
            complemento: complemento,
            numero: numero
        }),
*/