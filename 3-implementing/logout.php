<?php
if(isset($_COOKIE['tokenLogin'])){
    setcookie('tokenLogin', '', 1, '/');
}
header("location: ./login.php");