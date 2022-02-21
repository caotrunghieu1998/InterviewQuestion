<?php
class ResultModel
{
    // Field
    private $isSuccess;
    private $data;
    private $error;
    // Getter
    public function getIsSuccess(){
        return $this->isSuccess;
    }
    public function getData(){
        return $this->data;
    }
    public function getError(){
        return $this->error;
    }

    // Set Data
    public function setData($data)
    {
        $this->isSuccess =  true;
        $this->data = $data;
        $this->error = null;
    }

    // Set Error
    public function setError(string $error)
    {
        $this->isSuccess =  false;
        $this->data = null;
        $this->error = $error;
    }
}
