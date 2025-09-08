// 변수 선언
let num = 42;           // int
var name = "TOM";       // string
let isStudent = true;   // bool

// 배열
let color = ["rad", "green", "blue"];

// 객체
let person = { name : "Alice", age : 30};

// 함수
function greet(name){
    console.log("Hellol " + name + " ! ");
}
greet(person.name);
// 조건문
if(num > 30){
    console.log("Number is greater than 30");
}
else{
    console.log("Number is lower than 30")
}


// 반복문
for(var i = 0; i < 5; i++){
    console.log(i);
}
