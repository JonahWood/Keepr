import{_ as C,b,A as n,k as h,P as k,l as v,d as _,O as K,q as f,c as a,s as y,a as t,F as p,g as V,j as c,o,n as r,p as O,m as x}from"./index.f3562c27.js";const S={setup(){b(()=>{e()});async function e(){try{n.keeps=[],await h.getAllKeeps()}catch(s){k.error(s.message),v.error(s)}}return{keeps:_(()=>n.keeps),account:_(()=>n.account)}},components:{OCCreateK:K,OCCreateV:f}},l=e=>(O("data-v-4ae4565a"),e=e(),x(),e),w={key:0,class:"mb-2 d-flex justify-content-between"},A=l(()=>t("button",{class:"btn create-button ms-md-2 bg-gradient",type:"button","data-bs-toggle":"modal","data-bs-target":"#CreateKeep"},"Create Keep",-1)),I=l(()=>t("button",{class:"btn create-button ms-md-2 bg-gradient",type:"button","data-bs-toggle":"modal","data-bs-target":"#CreateVault"},"Create Vault",-1)),P=[A,I],B={class:"Kbody"},N={class:"row no-pad-marge"},j={class:"grid-container rounded"},F={class:"col-6 col-md-3"};function H(e,s,$,d,q,E){const i=r("KeepCard"),u=r("OCCreateK"),m=r("OCCreateV");return o(),a(p,null,[d.account.id?(o(),a("div",w,P)):y("",!0),t("div",B,[t("div",N,[t("section",j,[(o(!0),a(p,null,V(d.keeps,g=>(o(),a("div",F,[c(i,{keep:g},null,8,["keep"])]))),256))])])]),c(u),c(m)],64)}const M=C(S,[["render",H],["__scopeId","data-v-4ae4565a"]]);export{M as default};