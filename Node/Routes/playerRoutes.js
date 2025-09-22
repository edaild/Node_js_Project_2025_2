const express = require('express');
const fs = require('fs');
const router = express.Router();

// 초기 자원 설정

const initalResources = {
    metal : 500,
    crystal : 300,
    deuterirum : 100,
}

// 글러벌 플레이어 객체 초기화

global.players = {};

router.post('/register', (req, res) => {
    const {name, password} = req.body;

    if(global.player[name]){
        return res.status(400).send({message : '이미 등록된 사용자입니다.'});
    }

    global.players[name] ={
        playerName : name,
        password : password,
        resouces: {
            metal : 500,
            crystal : 300,
            deuterirum : 100
        },
        planets:[]
    }

    SaveResources();
    res.send({message : '등록 완료', player:name});
});


router.post('/login', (req, res) => {
    const {name, password} = req.body;

    if(!global.player[name]){
        return res.status(404).send({message: '플레이어를 찾을 수 없습니다.'})
    }

    if(password !== global.player[name].password){
        return res.status(401).send({message : '비밀번호가 틀렸습니다.'});
    }

    //응답 데이터
    const reqponsePayLod = {
        playerName : player.playerName,
        metal : player.resouces.metal,
        crystal : player.resouces.crystal,
        deuterium : player.resouces.deuterirum
    }

    console.log("Login response playerload : ", reqponsePayLod);
    res.send(reqponsePayLod);

});


module.exports = router;                // 라우터 등록