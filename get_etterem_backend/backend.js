const express = require('express');
const cors= require('cors');
const mysql= require('mysql');
const app= express();

const host='localhost';
const port=3000;

app.use(cors());
app.use(express.json());

let connection= mysql.createConnection({
    host:host,
    user:'root',
    password: '',
    database: 'etterem'
});

app.get('/etelek', (req, res)=>{
    connection.query('SELECT * FROM etlap', (err, results)=>{
        if(err){
            console.log(err);
            res.send("hiba");
        }
        else{
            console.log(results);
            res.send(results);
        }
    });
});

app.listen(port, host, ()=>{
    console.log(`IP: http://${host}:${port}`);
});