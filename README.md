## 아키텍처 개요
- SkillData (ScriptableObject): 쿨다운/이펙트 목록 등 스킬 정의
- Effect (모듈) 파이프라인: `CostEffect` → `ProjectileEffect` → `PullEffect` 순차 실행  
  - `CostEffect`: 자원(마나) 소모/체크  
  - `ProjectileEffect`: SphereCast 기반 투사체 발사  
  - `PullEffect`: 목표를 플레이어 앞 지점까지 끌어오기
- EffectContext: 시전자, 캐스트 지점, 방향, 히트 결과 공유

## 데이터 기반 튜닝
- `Assets/ScriptableObjects/Skills/`에서 파라미터(사거리, 속도, 비용 등) 수정으로 쉽게 밸런싱
- 새 스킬은 Effect 조합으로 손쉽게 확장 가능

## 구현사항
- 마우스 왼쪽 버튼 클릭 시 : 마우스 포인터 방향으로 그랩기능이 있는 투사체를 발사
- 마우스 오른쪽 버튼 클릭 시 : 마우스 포인터 위치로 캐릭터의 이동
