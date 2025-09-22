const express = require('express');
const fs = require('fs');                                                       // 파일 시스템 헤더에 추가
const PlayeRoutes = require('./Routes/playerRoutes')                             // 플레이어 라우터 폴더 추가
const app = express();
const port  = 4000;

app.use(express.json());                                                         // JSON 통신 설정
app.use('/api', PlayeRoutes);                                                    // API 라우트 설정
const resourrceFilePath = 'resources_json'                                      // 자원 저장 파일 경로

function loadResource(){
    if(fs.existsSync(resourrceFilePath)){                                       // 팡일 경로를 확인해서 파일이 있는지 확인
        const data = fs.readFileSync(resourrceFilePath);
        global.players = JSON.parse(data);                                      // 파일에서 로딩
    }else{
        global.players = {};                                                    // 초기화
    }                     
}

function saveResources(){
    fs.writeFileSync(resourrceFilePath, JSON.stringify(global.players, null, 2))            //JSON 파일로 저장
}

app.listen(port, ()=>{
    console.log(`서버가 http://localhost:${port}에서 실행 중 입니다.`);
})