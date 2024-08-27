var form = document.getElementById("formTest");

async function submitform(){
    debugger;
    console.log("hi");
    event.preventDefault();
    
    const formData = new FormData(form);
    var requist = await fetch("https://localhost:44348/api/Category",{
        method: "POST",
        body: formData
    });

    var data = requist;
} 
    