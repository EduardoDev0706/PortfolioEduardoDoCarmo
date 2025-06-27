// script.js
const API_URL = 'http://localhost:5136/api/produtos'; // Certifique-se que a porta corresponde à sua API

const productForm = document.getElementById('product-form');
const productIdInput = document.getElementById('product-id');
const nomeInput = document.getElementById('nome');
const precoInput = document.getElementById('preco');
const quantidadeInput = document.getElementById('quantidade');
const validadeInput = document.getElementById('validade');
const submitButton = document.getElementById('submit-button');
const productListContainer = document.getElementById('product-list-container');

// --- Funções de Carregamento de Produtos ---

async function fetchProducts() {
    try {
        const response = await fetch(API_URL); // Faz uma requisição GET para a API
        if (!response.ok) { // Verifica se a resposta HTTP foi bem-sucedida (status 2xx)
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const products = await response.json(); // Converte a resposta JSON em um objeto JavaScript
        displayProducts(products); // Exibe os produtos na página
    } catch (error) {
        console.error('Erro ao buscar produtos:', error);
        productListContainer.innerHTML = '<p>Erro ao carregar produtos. Verifique o console.</p>';
    }
}

function displayProducts(products) {
    productListContainer.innerHTML = ''; // Limpa a lista antes de adicionar os novos produtos

    if (products.length === 0) {
        productListContainer.innerHTML = '<p>Nenhum produto cadastrado.</p>';
        return;
    }

    products.forEach(product => {
        const productItem = document.createElement('div');
        productItem.className = 'product-item';
        productItem.innerHTML = `
            <div class="product-info">
                <h3>${product.nome} (ID: ${product.id})</h3>
                <p>Preço: R$ ${product.preco.toFixed(2)}</p>
                <p>Quantidade: ${product.quantidadeEmEstoque}</p>
                <p>Validade: ${product.dataValidade}</p>
            </div>
            <div class="product-actions">
                <button class="edit-btn" data-id="${product.id}">Editar</button>
                <button class="delete-btn" data-id="${product.id}">Excluir</button>
            </div>
        `;
        productListContainer.appendChild(productItem);
    });
}

// --- Funções de Adição/Edição de Produtos ---

productForm.addEventListener('submit', async (event) => {
    event.preventDefault(); // Impede o comportamento padrão de recarregar a página

    const id = parseInt(productIdInput.value); // Pega o ID (0 para novo, ID real para edição)
    const nome = nomeInput.value;
    const preco = parseFloat(precoInput.value);
    const quantidade = parseInt(quantidadeInput.value);
    const validade = validadeInput.value; // Formato YYYY-MM-DD

    const productData = {
        id: id,
        nome: nome,
        preco: preco,
        quantidadeEmEstoque: quantidade,
        dataValidade: validade
    };

    try {
        let response;
        if (id === 0) { // Se o ID for 0, é um novo produto (POST)
            // Para POST, não incluímos o ID na requisição se ele for gerado pelo backend.
            // Para este exemplo, manteremos o ID para simplicidade. No seu backend,
            // você tem a validação de ID duplicado.
            delete productData.id; // Remover o ID para o POST, assumindo que o backend o gerará
            response = await fetch(API_URL, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(productData) // Converte o objeto JavaScript para uma string JSON
            });
        } else { // Se o ID não for 0, é uma edição (PUT)
            response = await fetch(`${API_URL}/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(productData)
            });
        }

        if (!response.ok) {
            const errorText = await response.text(); // Pega a mensagem de erro do backend
            throw new Error(`HTTP error! status: ${response.status} - ${errorText}`);
        }

        // Limpa o formulário e recarrega a lista de produtos
        productForm.reset();
        productIdInput.value = '0'; // Reseta o ID oculto para 0
        submitButton.textContent = 'Adicionar Produto'; // Reseta o texto do botão
        fetchProducts(); // Recarrega a lista para mostrar a mudança
    } catch (error) {
        console.error('Erro ao salvar produto:', error);
        alert(`Erro ao salvar produto: ${error.message}`);
    }
});


// --- Inicialização ---
fetchProducts(); // Carrega os produtos assim que a página é carregada