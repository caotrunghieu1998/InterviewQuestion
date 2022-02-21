<?php
require_once './config/database.php';
require_once './config/config.php';
spl_autoload_register(function ($class_name) {
    require './app/models/' . $class_name . '.php';
});
$userModel = new UserModel();

// Processing Register
if (isset($_POST['formRegisterSubmit'])) {
    $register = $userModel->register($_POST['userName'], $_POST['password']);
    if (!$register->getIsSuccess()) {
        $error = $register->getError();
    } else {
        echo "<script>alert('Register Success !!');window.location.href = './login.php?userName=".$_POST['userName']."';</script>";
    }
}

// Check is user logged
if (isset($_COOKIE['tokenLogin'])) {
    if (!is_null($userModel->verticalToken($_COOKIE['tokenLogin']))) {
        echo "<script>
        alert('You are logged in !!');
        window.location.href = './index.php';
        </script>";
    }else{
        setcookie('tokenLogin', '', 1, '/');
    }
}
?>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
</head>

<body>
    <div class="container">
        <h1 style="text-align: center;">Register</h1>
        <!-- Form Start -->
        <form method="POST">
            <div class="form-group">
                <label>User Name</label>
                <input type="text" class="form-control" placeholder="UserName" name="userName" required>
            </div>
            <div class="form-group">
                <label>Password</label>
                <input type="password" class="form-control" placeholder="Password" name="password" required>
            </div>
            <small id="emailHelp" class="form-text text-muted" style="color: red !important;font-size: 20px;">
                <?php if (isset($error)) echo $error; ?>
            </small>
            <button type="submit" class="btn btn-primary" name="formRegisterSubmit">REGISTER</button>
        </form>
        <div class="login-btn" style="padding-top: 20px;">
            <a href="./login.php">
                <- You had account? Login here</a>
        </div>
    </div>
    <!-- Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>

</html>