import{_ as K,b as S,u as a,c as r,a as e,t as i,F as x,g as w,A as c,i as b,P as d,l as _,k as A,z as B,o as n,j as I,x as C,p as M,m as D,n as P}from"./index.f3562c27.js";const H={setup(){const o=C();async function p(){try{c.vaults=[];const s=o.params.profileId;await b.getProfileVaults(s)}catch(s){d.error(s.message),_.error(s)}}async function u(){try{c.keeps=[];const s=o.params.profileId;await A.getProfileKeeps(s)}catch(s){d.error(s.message),_.error(s)}}async function t(){try{const s=o.params.profileId;await B.getProfile(s)}catch(s){d.error(s.message),_.error(s)}}return S(()=>{t(),u(),p()}),{account:a(()=>c.account),profile:a(()=>c.profile),keeps:a(()=>c.keeps),vaults:a(()=>c.vaults),activeKeep:a(()=>c.activeKeep)}}},k=o=>(M("data-v-3a7da431"),o=o(),D(),o),F={class:"container"},G={class:"row justify-content-center"},N={class:"col-md-10 mt-md-3 d-flex justify-content-center"},q=["src"],z={class:"row justify-content-center"},E={class:"col-md-2 d-flex justify-content-center"},L=["src"],R={class:"row justify-content-center"},W={class:"col-md-1 d-flex justify-content-center"},Y={class:"row justify-content-center"},J={class:"col-md-4 d-flex justify-content-center"},O={class:"lengths"},Q={class:"row mb-5"},T=k(()=>e("div",{class:"col-12"},[e("h2",null,"Vaults")],-1)),U={class:"col-md-3"},X={class:"row"},Z=k(()=>e("div",{class:"col-12"},[e("h2",null,"Keeps")],-1)),$={class:"col-md-3"};function ee(o,p,u,t,s,se){var f,m,h,v,g,y;const j=P("VaultCard"),V=P("KeepCardAcc");return n(),r("div",F,[e("section",G,[e("div",N,[e("img",{class:"coverImg img-fluid rounded",src:(f=t.profile)!=null&&f.coverImg?(m=t.profile)==null?void 0:m.coverImg:"https://images.unsplash.com/photo-1521459467264-802e2ef3141f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80",alt:""},null,8,q)])]),e("section",z,[e("div",E,[e("img",{class:"elevation-3 accImg",src:(h=t.profile)==null?void 0:h.picture,alt:""},null,8,L)])]),e("section",R,[e("div",W,[e("h1",null,i((v=t.profile)==null?void 0:v.name),1)])]),e("section",Y,[e("div",J,[e("h4",O,i((g=t.vaults)==null?void 0:g.length)+" Vaults | "+i((y=t.keeps)==null?void 0:y.length)+" Keeps",1)])]),e("section",Q,[T,(n(!0),r(x,null,w(t.vaults,l=>(n(),r("div",U,[I(j,{vault:l},null,8,["vault"])]))),256))]),e("section",X,[Z,(n(!0),r(x,null,w(t.keeps,l=>(n(),r("div",$,[I(V,{keep:l},null,8,["keep"])]))),256))])])}const oe=K(H,[["render",ee],["__scopeId","data-v-3a7da431"]]);export{oe as default};