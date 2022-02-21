<?php

// require 'ResultModel.php';

class UserModel extends Db
{
    // Get all User Value
    public function getAllUserValue()
    {
        $sql = parent::$connection->prepare("SELECT * FROM `users`");
        return parent::select($sql);
    }

    // Vertical token
    public function verticalToken(string $token){
        foreach ($this->getAllUserValue() as $user) {
            if (md5(md5("CaoTrungHieu".$user['id'])) == $token) {
                return $user;
            }
        }
        return null;
    }

    // Login function
    public function login(string $userName, string $password)
    {
        $result = new ResultModel();
        $password = md5($password);
        $sql = parent::$connection->prepare("SELECT * FROM `users` 
        WHERE `userName` = ? AND `password` = ? ");
        $sql->bind_param('ss', $userName, $password);
        $value = parent::select($sql);
        if (isset($value[0])) {
            $result->setData($value[0]);
        } else {
            $result->setError("Wrong at User Name or Password !!");
        }
        return $result;
    }

    // Register function
    public function register(string $userName, string $password)
    {
        $result = new ResultModel();
        foreach ($this->getAllUserValue() as $user) {
            if ($user['userName'] == $userName) {
                $result->setError("User name has Exist");
                return $result;
            }
        }
        try {
            $password = md5($password);
            $sql = parent::$connection->prepare("INSERT INTO `users`(`userName`, `password`) 
        VALUES (?,?)");
            $sql->bind_param('ss', $userName, $password);
            if ($sql->execute()) {
                $result->setData("Register Success !!");
            } else {
                $result->setError("Wrong at User Name or Password !!");
            }
        } catch (Exception $ex) {
            $result->setError($ex->getMessage());
        }
        return $result;
    }
}
