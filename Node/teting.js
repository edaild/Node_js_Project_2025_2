const express = require('express');
const mysql = require('mysql2/promise');
const PORT = 4000;

const app = express();
const pool = mysql.createPool({
    host : 'localhost',
    name : 'root',
    password : '1234',
    database : 'test'
});

app.use(express.json());

a

app.listen(PORT, ()=>{
    console.log("서버 실행중");
});

