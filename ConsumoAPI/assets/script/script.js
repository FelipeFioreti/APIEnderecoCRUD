
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

    if(cep.length !== 8){
        alert("CEP inválido. Digite um CEP com 8 digítos.");
        return;
    }

    fetch(`https://viacep.com.br/ws/${cep}/json/`).then(response => response.json()).then(data => {
        if(data.erro){
            alert("CEP não encontrado.");
            return;
        }

        document.getElementById("logradouro").value = data.logradouro || "";

        document.getElementById("bairro").value = data.bairro || ""; 
        
        document.getElementById("cidade").value = data.localidade || "";

        document.getElementById("estado").value = data.uf || "";
    }).catch(error => {
        alert("Erro ao buscar CEP");
    })
}

