var n = localStorage.getItem("cartID");
var url = `https://localhost:44348/api/Cart/${n}`;
var tbody = document.getElementById("tbody");
async function allCart() {

    var requist = await fetch(url);
    var response = await requist.json();
    response.forEach(element => {
        var row = document.createElement("tr");
        row.innerHTML = `
        <td><img style="width: 100px;" src="../../Back_end/TASK21-08/Uploads/${element.product.productImage}" alt="${element.product.productImage}"></td>
        <td>${element.product.productName}</td>
        <td>$${element.product.price}</td>
        <td>${element.quantity}</td>
        <td>${element.product.description}</td>
        `;
        tbody.appendChild(row);
    });
};

allCart();
