var form = document.getElementById("formTest");

async function submitform(){
    debugger;
    event.preventDefault();
    
    const formData = new FormData(form);
    var requist = await fetch("https://localhost:44348/api/Products",{
        method: "POST",
        body: formData
    });

    var data = requist;
} 


const url = "https://localhost:44348/api/Category";

async function allCategory() {

    var requist = await fetch(url);
    var response = await requist.json();

    var CategoryId = document.getElementById("CategoryId");

    response.forEach(element => {
        


    CategoryId.innerHTML += `
    <option value="${element.categoryId}">${element.categoryName}</option>

    `
    });
    
}
allCategory();