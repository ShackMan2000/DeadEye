#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};

// System.Comparison`1<UnityEngine.RaycastHit>
struct Comparison_1_t5A3269D71CFF48B1462FED00091AE93BBABC91E7;
// System.Comparison`1<UnityEngine.EventSystems.RaycastResult>
struct Comparison_1_t9FCAC8C8CE160A96C5AAD2DE1D353DCE8A2FEEFC;
// System.Collections.Generic.List`1<UnityEngine.EventSystems.BaseInputModule>
struct List_1_tA5BDE435C735A082941CD33D212F97F4AE9FA55F;
// System.Collections.Generic.List`1<UnityEngine.EventSystems.EventSystem>
struct List_1_tF2FE88545EFEC788CAAE6C74EC2F78E937FCCAC3;
// System.Collections.Generic.List`1<UnityEngine.GameObject>
struct List_1_tB951CE80B58D1BF9650862451D8DAD8C231F207B;
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
// UnityEngine.EventSystems.BaseEventData
struct BaseEventData_tE03A848325C0AE8E76C6CA15FD86395EBF83364F;
// UnityEngine.EventSystems.BaseInputModule
struct BaseInputModule_tF3B7C22AF1419B2AC9ECE6589357DC1B88ED96B1;
// UnityEngine.EventSystems.BaseRaycaster
struct BaseRaycaster_t7DC8158FD3CA0193455344379DD5FF7CD5F1F832;
// UnityEngine.EventSystems.EventSystem
struct EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707;
// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F;
// UnityEngine.EventSystems.OVRPointerEventData
struct OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D;
// UnityEngine.EventSystems.PointerEventData
struct PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB;
// System.String
struct String_t;
// System.Text.StringBuilder
struct StringBuilder_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c
struct U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D;

IL2CPP_EXTERN_C RuntimeClass* Assert_tDC16963451AC4364803739B73A4477ADCB365863_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringBuilder_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral06A17D93E6D67BEA37EBBF3A9D7FC06B40689CD1;
IL2CPP_EXTERN_C String_t* _stringLiteral1EC5A90CC65C8BDA58643C956BA4E6F8E4436A5A;
IL2CPP_EXTERN_C String_t* _stringLiteral1FF1E10A290EC11AB4C6B8CA7BD71BA07C515124;
IL2CPP_EXTERN_C String_t* _stringLiteral709C1BE385364BF7215290F6C80B9E13ED6C07AB;
IL2CPP_EXTERN_C String_t* _stringLiteral97949C809FAE8AD49BF4DA35A46951F99B00E5E1;
IL2CPP_EXTERN_C String_t* _stringLiteral9C5CA5F3D440697E7CE47F45B0AE3B6DB74C2054;
IL2CPP_EXTERN_C String_t* _stringLiteralAAD329BEE4AA4299DC498EF86EE4D802F5F77951;
IL2CPP_EXTERN_C String_t* _stringLiteralB4705CCB6B015DADE9B7063D15E59D6BAE057C37;
IL2CPP_EXTERN_C String_t* _stringLiteralBC250738CF6553169DE970EACBEDFB060B58A41B;
IL2CPP_EXTERN_C String_t* _stringLiteralDB1334B07CE2A0153E77054CF8FA3829A2097735;
IL2CPP_EXTERN_C const RuntimeMethod* Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590_RuntimeMethod_var;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// UnityEngine.EventSystems.AbstractEventData
struct AbstractEventData_tAE1A127ED657117548181D29FFE4B1B14D8E67F7  : public RuntimeObject
{
	// System.Boolean UnityEngine.EventSystems.AbstractEventData::m_Used
	bool ___m_Used_0;
};

// UnityEngine.EventSystems.PointerEventDataExtension
struct PointerEventDataExtension_t4335B94AF6A64806EED176B63B67CAD2BB98E0EA  : public RuntimeObject
{
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

// System.Text.StringBuilder
struct StringBuilder_t  : public RuntimeObject
{
	// System.Char[] System.Text.StringBuilder::m_ChunkChars
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___m_ChunkChars_0;
	// System.Text.StringBuilder System.Text.StringBuilder::m_ChunkPrevious
	StringBuilder_t* ___m_ChunkPrevious_1;
	// System.Int32 System.Text.StringBuilder::m_ChunkLength
	int32_t ___m_ChunkLength_2;
	// System.Int32 System.Text.StringBuilder::m_ChunkOffset
	int32_t ___m_ChunkOffset_3;
	// System.Int32 System.Text.StringBuilder::m_MaxCapacity
	int32_t ___m_MaxCapacity_4;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c
struct U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D  : public RuntimeObject
{
};

// UnityEngine.EventSystems.BaseEventData
struct BaseEventData_tE03A848325C0AE8E76C6CA15FD86395EBF83364F  : public AbstractEventData_tAE1A127ED657117548181D29FFE4B1B14D8E67F7
{
	// UnityEngine.EventSystems.EventSystem UnityEngine.EventSystems.BaseEventData::m_EventSystem
	EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707* ___m_EventSystem_1;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// UnityEngine.Vector2
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	// System.Single UnityEngine.Vector2::x
	float ___x_0;
	// System.Single UnityEngine.Vector2::y
	float ___y_1;
};

// UnityEngine.Vector3
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	// System.Single UnityEngine.Vector3::x
	float ___x_2;
	// System.Single UnityEngine.Vector3::y
	float ___y_3;
	// System.Single UnityEngine.Vector3::z
	float ___z_4;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=24
struct __StaticArrayInitTypeSizeU3D24_tB80B93638C5B131A2ECBFB2B90A6F7C524560B75 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D24_tB80B93638C5B131A2ECBFB2B90A6F7C524560B75__padding[24];
	};
};

// UnityEngine.EventSystems.EventSystem/UIToolkitOverrideConfig
struct UIToolkitOverrideConfig_t4E6B4528E38BCA7DA72C45424634806200A50182 
{
	// UnityEngine.EventSystems.EventSystem UnityEngine.EventSystems.EventSystem/UIToolkitOverrideConfig::activeEventSystem
	EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707* ___activeEventSystem_0;
	// System.Boolean UnityEngine.EventSystems.EventSystem/UIToolkitOverrideConfig::sendEvents
	bool ___sendEvents_1;
	// System.Boolean UnityEngine.EventSystems.EventSystem/UIToolkitOverrideConfig::createPanelGameObjectsOnStart
	bool ___createPanelGameObjectsOnStart_2;
};
// Native definition for P/Invoke marshalling of UnityEngine.EventSystems.EventSystem/UIToolkitOverrideConfig
struct UIToolkitOverrideConfig_t4E6B4528E38BCA7DA72C45424634806200A50182_marshaled_pinvoke
{
	EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707* ___activeEventSystem_0;
	int32_t ___sendEvents_1;
	int32_t ___createPanelGameObjectsOnStart_2;
};
// Native definition for COM marshalling of UnityEngine.EventSystems.EventSystem/UIToolkitOverrideConfig
struct UIToolkitOverrideConfig_t4E6B4528E38BCA7DA72C45424634806200A50182_marshaled_com
{
	EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707* ___activeEventSystem_0;
	int32_t ___sendEvents_1;
	int32_t ___createPanelGameObjectsOnStart_2;
};

// <PrivateImplementationDetails>
struct U3CPrivateImplementationDetailsU3E_t8CCC1D019897BE2F4568BD89DEABE32FE4F8114C  : public RuntimeObject
{
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// UnityEngine.Ray
struct Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 
{
	// UnityEngine.Vector3 UnityEngine.Ray::m_Origin
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Origin_0;
	// UnityEngine.Vector3 UnityEngine.Ray::m_Direction
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Direction_1;
};

// UnityEngine.RaycastHit
struct RaycastHit_t6F30BD0B38B56401CA833A1B87BD74F2ACD2F2B5 
{
	// UnityEngine.Vector3 UnityEngine.RaycastHit::m_Point
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Point_0;
	// UnityEngine.Vector3 UnityEngine.RaycastHit::m_Normal
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Normal_1;
	// System.UInt32 UnityEngine.RaycastHit::m_FaceID
	uint32_t ___m_FaceID_2;
	// System.Single UnityEngine.RaycastHit::m_Distance
	float ___m_Distance_3;
	// UnityEngine.Vector2 UnityEngine.RaycastHit::m_UV
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___m_UV_4;
	// System.Int32 UnityEngine.RaycastHit::m_Collider
	int32_t ___m_Collider_5;
};

// UnityEngine.EventSystems.RaycastResult
struct RaycastResult_tEC6A7B7CABA99C386F054F01E498AEC426CF8023 
{
	// UnityEngine.GameObject UnityEngine.EventSystems.RaycastResult::m_GameObject
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___m_GameObject_0;
	// UnityEngine.EventSystems.BaseRaycaster UnityEngine.EventSystems.RaycastResult::module
	BaseRaycaster_t7DC8158FD3CA0193455344379DD5FF7CD5F1F832* ___module_1;
	// System.Single UnityEngine.EventSystems.RaycastResult::distance
	float ___distance_2;
	// System.Single UnityEngine.EventSystems.RaycastResult::index
	float ___index_3;
	// System.Int32 UnityEngine.EventSystems.RaycastResult::depth
	int32_t ___depth_4;
	// System.Int32 UnityEngine.EventSystems.RaycastResult::sortingLayer
	int32_t ___sortingLayer_5;
	// System.Int32 UnityEngine.EventSystems.RaycastResult::sortingOrder
	int32_t ___sortingOrder_6;
	// UnityEngine.Vector3 UnityEngine.EventSystems.RaycastResult::worldPosition
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldPosition_7;
	// UnityEngine.Vector3 UnityEngine.EventSystems.RaycastResult::worldNormal
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldNormal_8;
	// UnityEngine.Vector2 UnityEngine.EventSystems.RaycastResult::screenPosition
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___screenPosition_9;
	// System.Int32 UnityEngine.EventSystems.RaycastResult::displayIndex
	int32_t ___displayIndex_10;
};
// Native definition for P/Invoke marshalling of UnityEngine.EventSystems.RaycastResult
struct RaycastResult_tEC6A7B7CABA99C386F054F01E498AEC426CF8023_marshaled_pinvoke
{
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___m_GameObject_0;
	BaseRaycaster_t7DC8158FD3CA0193455344379DD5FF7CD5F1F832* ___module_1;
	float ___distance_2;
	float ___index_3;
	int32_t ___depth_4;
	int32_t ___sortingLayer_5;
	int32_t ___sortingOrder_6;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldPosition_7;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldNormal_8;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___screenPosition_9;
	int32_t ___displayIndex_10;
};
// Native definition for COM marshalling of UnityEngine.EventSystems.RaycastResult
struct RaycastResult_tEC6A7B7CABA99C386F054F01E498AEC426CF8023_marshaled_com
{
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___m_GameObject_0;
	BaseRaycaster_t7DC8158FD3CA0193455344379DD5FF7CD5F1F832* ___module_1;
	float ___distance_2;
	float ___index_3;
	int32_t ___depth_4;
	int32_t ___sortingLayer_5;
	int32_t ___sortingOrder_6;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldPosition_7;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___worldNormal_8;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___screenPosition_9;
	int32_t ___displayIndex_10;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// UnityEngine.EventSystems.PointerEventData
struct PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB  : public BaseEventData_tE03A848325C0AE8E76C6CA15FD86395EBF83364F
{
	// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::<pointerEnter>k__BackingField
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___U3CpointerEnterU3Ek__BackingField_2;
	// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::m_PointerPress
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___m_PointerPress_3;
	// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::<lastPress>k__BackingField
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___U3ClastPressU3Ek__BackingField_4;
	// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::<rawPointerPress>k__BackingField
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___U3CrawPointerPressU3Ek__BackingField_5;
	// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::<pointerDrag>k__BackingField
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___U3CpointerDragU3Ek__BackingField_6;
	// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::<pointerClick>k__BackingField
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___U3CpointerClickU3Ek__BackingField_7;
	// UnityEngine.EventSystems.RaycastResult UnityEngine.EventSystems.PointerEventData::<pointerCurrentRaycast>k__BackingField
	RaycastResult_tEC6A7B7CABA99C386F054F01E498AEC426CF8023 ___U3CpointerCurrentRaycastU3Ek__BackingField_8;
	// UnityEngine.EventSystems.RaycastResult UnityEngine.EventSystems.PointerEventData::<pointerPressRaycast>k__BackingField
	RaycastResult_tEC6A7B7CABA99C386F054F01E498AEC426CF8023 ___U3CpointerPressRaycastU3Ek__BackingField_9;
	// System.Collections.Generic.List`1<UnityEngine.GameObject> UnityEngine.EventSystems.PointerEventData::hovered
	List_1_tB951CE80B58D1BF9650862451D8DAD8C231F207B* ___hovered_10;
	// System.Boolean UnityEngine.EventSystems.PointerEventData::<eligibleForClick>k__BackingField
	bool ___U3CeligibleForClickU3Ek__BackingField_11;
	// System.Int32 UnityEngine.EventSystems.PointerEventData::<pointerId>k__BackingField
	int32_t ___U3CpointerIdU3Ek__BackingField_12;
	// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::<position>k__BackingField
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CpositionU3Ek__BackingField_13;
	// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::<delta>k__BackingField
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CdeltaU3Ek__BackingField_14;
	// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::<pressPosition>k__BackingField
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CpressPositionU3Ek__BackingField_15;
	// UnityEngine.Vector3 UnityEngine.EventSystems.PointerEventData::<worldPosition>k__BackingField
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CworldPositionU3Ek__BackingField_16;
	// UnityEngine.Vector3 UnityEngine.EventSystems.PointerEventData::<worldNormal>k__BackingField
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___U3CworldNormalU3Ek__BackingField_17;
	// System.Single UnityEngine.EventSystems.PointerEventData::<clickTime>k__BackingField
	float ___U3CclickTimeU3Ek__BackingField_18;
	// System.Int32 UnityEngine.EventSystems.PointerEventData::<clickCount>k__BackingField
	int32_t ___U3CclickCountU3Ek__BackingField_19;
	// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::<scrollDelta>k__BackingField
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CscrollDeltaU3Ek__BackingField_20;
	// System.Boolean UnityEngine.EventSystems.PointerEventData::<useDragThreshold>k__BackingField
	bool ___U3CuseDragThresholdU3Ek__BackingField_21;
	// System.Boolean UnityEngine.EventSystems.PointerEventData::<dragging>k__BackingField
	bool ___U3CdraggingU3Ek__BackingField_22;
	// UnityEngine.EventSystems.PointerEventData/InputButton UnityEngine.EventSystems.PointerEventData::<button>k__BackingField
	int32_t ___U3CbuttonU3Ek__BackingField_23;
	// System.Single UnityEngine.EventSystems.PointerEventData::<pressure>k__BackingField
	float ___U3CpressureU3Ek__BackingField_24;
	// System.Single UnityEngine.EventSystems.PointerEventData::<tangentialPressure>k__BackingField
	float ___U3CtangentialPressureU3Ek__BackingField_25;
	// System.Single UnityEngine.EventSystems.PointerEventData::<altitudeAngle>k__BackingField
	float ___U3CaltitudeAngleU3Ek__BackingField_26;
	// System.Single UnityEngine.EventSystems.PointerEventData::<azimuthAngle>k__BackingField
	float ___U3CazimuthAngleU3Ek__BackingField_27;
	// System.Single UnityEngine.EventSystems.PointerEventData::<twist>k__BackingField
	float ___U3CtwistU3Ek__BackingField_28;
	// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::<radius>k__BackingField
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CradiusU3Ek__BackingField_29;
	// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::<radiusVariance>k__BackingField
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___U3CradiusVarianceU3Ek__BackingField_30;
	// System.Boolean UnityEngine.EventSystems.PointerEventData::<fullyExited>k__BackingField
	bool ___U3CfullyExitedU3Ek__BackingField_31;
	// System.Boolean UnityEngine.EventSystems.PointerEventData::<reentered>k__BackingField
	bool ___U3CreenteredU3Ek__BackingField_32;
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// UnityEngine.EventSystems.OVRPointerEventData
struct OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D  : public PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB
{
	// UnityEngine.Ray UnityEngine.EventSystems.OVRPointerEventData::worldSpaceRay
	Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 ___worldSpaceRay_33;
	// UnityEngine.Vector2 UnityEngine.EventSystems.OVRPointerEventData::swipeStart
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___swipeStart_34;
};

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// UnityEngine.EventSystems.UIBehaviour
struct UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};

// UnityEngine.EventSystems.EventSystem
struct EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707  : public UIBehaviour_tB9D4295827BD2EEDEF0749200C6CA7090C742A9D
{
	// System.Collections.Generic.List`1<UnityEngine.EventSystems.BaseInputModule> UnityEngine.EventSystems.EventSystem::m_SystemInputModules
	List_1_tA5BDE435C735A082941CD33D212F97F4AE9FA55F* ___m_SystemInputModules_4;
	// UnityEngine.EventSystems.BaseInputModule UnityEngine.EventSystems.EventSystem::m_CurrentInputModule
	BaseInputModule_tF3B7C22AF1419B2AC9ECE6589357DC1B88ED96B1* ___m_CurrentInputModule_5;
	// UnityEngine.GameObject UnityEngine.EventSystems.EventSystem::m_FirstSelected
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___m_FirstSelected_7;
	// System.Boolean UnityEngine.EventSystems.EventSystem::m_sendNavigationEvents
	bool ___m_sendNavigationEvents_8;
	// System.Int32 UnityEngine.EventSystems.EventSystem::m_DragThreshold
	int32_t ___m_DragThreshold_9;
	// UnityEngine.GameObject UnityEngine.EventSystems.EventSystem::m_CurrentSelected
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___m_CurrentSelected_10;
	// System.Boolean UnityEngine.EventSystems.EventSystem::m_HasFocus
	bool ___m_HasFocus_11;
	// System.Boolean UnityEngine.EventSystems.EventSystem::m_SelectionGuard
	bool ___m_SelectionGuard_12;
	// UnityEngine.EventSystems.BaseEventData UnityEngine.EventSystems.EventSystem::m_DummyData
	BaseEventData_tE03A848325C0AE8E76C6CA15FD86395EBF83364F* ___m_DummyData_13;
};

// UnityEngine.EventSystems.PointerEventDataExtension

// UnityEngine.EventSystems.PointerEventDataExtension

// System.String
struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.String

// System.Text.StringBuilder

// System.Text.StringBuilder

// UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c
struct U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_StaticFields
{
	// UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::<>9
	U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D* ___U3CU3E9_0;
	// System.Comparison`1<UnityEngine.RaycastHit> UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::<>9__15_0
	Comparison_1_t5A3269D71CFF48B1462FED00091AE93BBABC91E7* ___U3CU3E9__15_0_1;
	// System.Comparison`1<UnityEngine.RaycastHit> UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::<>9__16_0
	Comparison_1_t5A3269D71CFF48B1462FED00091AE93BBABC91E7* ___U3CU3E9__16_0_2;
};

// UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Boolean

// System.Int32

// System.Int32

// System.Single

// System.Single

// UnityEngine.Vector2
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_StaticFields
{
	// UnityEngine.Vector2 UnityEngine.Vector2::zeroVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___zeroVector_2;
	// UnityEngine.Vector2 UnityEngine.Vector2::oneVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___oneVector_3;
	// UnityEngine.Vector2 UnityEngine.Vector2::upVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___upVector_4;
	// UnityEngine.Vector2 UnityEngine.Vector2::downVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___downVector_5;
	// UnityEngine.Vector2 UnityEngine.Vector2::leftVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___leftVector_6;
	// UnityEngine.Vector2 UnityEngine.Vector2::rightVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___rightVector_7;
	// UnityEngine.Vector2 UnityEngine.Vector2::positiveInfinityVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___positiveInfinityVector_8;
	// UnityEngine.Vector2 UnityEngine.Vector2::negativeInfinityVector
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___negativeInfinityVector_9;
};

// UnityEngine.Vector2

// System.Void

// System.Void

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=24

// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=24

// <PrivateImplementationDetails>
struct U3CPrivateImplementationDetailsU3E_t8CCC1D019897BE2F4568BD89DEABE32FE4F8114C_StaticFields
{
	// <PrivateImplementationDetails>/__StaticArrayInitTypeSize=24 <PrivateImplementationDetails>::CD9A54ED1F18BF97DB08914E280EA7349E11CA2C4885A4D8052552CEBA84208D
	__StaticArrayInitTypeSizeU3D24_tB80B93638C5B131A2ECBFB2B90A6F7C524560B75 ___CD9A54ED1F18BF97DB08914E280EA7349E11CA2C4885A4D8052552CEBA84208D_0;
};

// <PrivateImplementationDetails>

// UnityEngine.Ray

// UnityEngine.Ray

// UnityEngine.RaycastHit

// UnityEngine.RaycastHit

// UnityEngine.GameObject

// UnityEngine.GameObject

// UnityEngine.EventSystems.PointerEventData

// UnityEngine.EventSystems.PointerEventData

// UnityEngine.EventSystems.OVRPointerEventData

// UnityEngine.EventSystems.OVRPointerEventData

// UnityEngine.EventSystems.EventSystem
struct EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707_StaticFields
{
	// System.Collections.Generic.List`1<UnityEngine.EventSystems.EventSystem> UnityEngine.EventSystems.EventSystem::m_EventSystems
	List_1_tF2FE88545EFEC788CAAE6C74EC2F78E937FCCAC3* ___m_EventSystems_6;
	// System.Comparison`1<UnityEngine.EventSystems.RaycastResult> UnityEngine.EventSystems.EventSystem::s_RaycastComparer
	Comparison_1_t9FCAC8C8CE160A96C5AAD2DE1D353DCE8A2FEEFC* ___s_RaycastComparer_14;
	// UnityEngine.EventSystems.EventSystem/UIToolkitOverrideConfig UnityEngine.EventSystems.EventSystem::s_UIToolkitOverride
	UIToolkitOverrideConfig_t4E6B4528E38BCA7DA72C45424634806200A50182 ___s_UIToolkitOverride_15;
};

// UnityEngine.EventSystems.EventSystem
#ifdef __clang__
#pragma clang diagnostic pop
#endif


// System.Void UnityEngine.Assertions.Assert::IsNotNull<System.Object>(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Assert_IsNotNull_TisRuntimeObject_mFA75318800124DED2E924476F16DD129394A20AC_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) ;

// System.Void UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__ctor_mFC1568EC2B965777FBA11D46651A32FB41491E2A (U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D* __this, const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Single UnityEngine.RaycastHit::get_distance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float RaycastHit_get_distance_m035194B0E9BB6229259CFC43B095A9C8E5011C78 (RaycastHit_t6F30BD0B38B56401CA833A1B87BD74F2ACD2F2B5* __this, const RuntimeMethod* method) ;
// System.Int32 System.Single::CompareTo(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Single_CompareTo_m06F7868162EB392D3E99103D1A0BD27463C9E66F (float* __this, float ___0_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.EventSystems.PointerEventData::.ctor(UnityEngine.EventSystems.EventSystem)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PointerEventData__ctor_m63837790B68893F0022CCEFEF26ADD55A975F71C (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707* ___0_eventSystem, const RuntimeMethod* method) ;
// System.Void System.Text.StringBuilder::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D (StringBuilder_t* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::get_position()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 PointerEventData_get_position_m5BE71C28EB72EFB8435749E4E6E839213AEF458C_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.Vector2::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Vector2_ToString_mB47B29ECB21FA3A4ACEABEFA18077A5A6BBCCB27 (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
// System.Text.StringBuilder System.Text.StringBuilder::AppendLine(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR StringBuilder_t* StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88 (StringBuilder_t* __this, String_t* ___0_value, const RuntimeMethod* method) ;
// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventData::get_delta()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 PointerEventData_get_delta_m7DC87C01EAE1D10282C37842ED215FDBFE2C1C5B_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.EventSystems.PointerEventData::get_eligibleForClick()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool PointerEventData_get_eligibleForClick_m4B01A1640C694FD7421BDFB19CF763BC85672C8E_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// System.String System.Boolean::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63 (bool* __this, const RuntimeMethod* method) ;
// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::get_pointerEnter()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* PointerEventData_get_pointerEnter_m6CE76D5C0C36C4666CDDE348B57885C52D495A4B_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::get_pointerPress()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* PointerEventData_get_pointerPress_mEE815DDB67E40AA587090BCCE0E3CABA6405C50A (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::get_lastPress()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* PointerEventData_get_lastPress_m46720C62503214A44EE947679A8BA307BC2AEEDC_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// UnityEngine.GameObject UnityEngine.EventSystems.PointerEventData::get_pointerDrag()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* PointerEventData_get_pointerDrag_m36BF08A32216845A8095C5F74DFE6C9959A11E96_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.Ray::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Ray_ToString_m06274331D92120539B4C6E0D3747EE620DB468E5 (Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.EventSystems.PointerEventData::get_useDragThreshold()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool PointerEventData_get_useDragThreshold_m3ED1F39E71630C9AB1F340C92F8FA39AA489E1C5_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.Assertions.Assert::IsNotNull<UnityEngine.EventSystems.OVRPointerEventData>(T)
inline void Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590 (OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* ___0_value, const RuntimeMethod* method)
{
	((  void (*) (OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D*, const RuntimeMethod*))Assert_IsNotNull_TisRuntimeObject_mFA75318800124DED2E924476F16DD129394A20AC_gshared)(___0_value, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__cctor_mA489E90CFBC65EE9F35B42C72ECA8037362EF497 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D* L_0 = (U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D*)il2cpp_codegen_object_new(U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__ctor_mFC1568EC2B965777FBA11D46651A32FB41491E2A(L_0, NULL);
		((U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_il2cpp_TypeInfo_var))->___U3CU3E9_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D_il2cpp_TypeInfo_var))->___U3CU3E9_0), (void*)L_0);
		return;
	}
}
// System.Void UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__ctor_mFC1568EC2B965777FBA11D46651A32FB41491E2A (U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Int32 UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::<Raycast>b__15_0(UnityEngine.RaycastHit,UnityEngine.RaycastHit)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t U3CU3Ec_U3CRaycastU3Eb__15_0_mEFC66B60EE677A040D1C002373D4CF3C5DFF97FF (U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D* __this, RaycastHit_t6F30BD0B38B56401CA833A1B87BD74F2ACD2F2B5 ___0_r1, RaycastHit_t6F30BD0B38B56401CA833A1B87BD74F2ACD2F2B5 ___1_r2, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		// System.Array.Sort(hits, (r1, r2) => r1.distance.CompareTo(r2.distance));
		float L_0;
		L_0 = RaycastHit_get_distance_m035194B0E9BB6229259CFC43B095A9C8E5011C78((&___0_r1), NULL);
		V_0 = L_0;
		float L_1;
		L_1 = RaycastHit_get_distance_m035194B0E9BB6229259CFC43B095A9C8E5011C78((&___1_r2), NULL);
		int32_t L_2;
		L_2 = Single_CompareTo_m06F7868162EB392D3E99103D1A0BD27463C9E66F((&V_0), L_1, NULL);
		return L_2;
	}
}
// System.Int32 UnityEngine.EventSystems.OVRPhysicsRaycaster/<>c::<Spherecast>b__16_0(UnityEngine.RaycastHit,UnityEngine.RaycastHit)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t U3CU3Ec_U3CSpherecastU3Eb__16_0_m01ADCEABB9904FA2BE7802AEA7CD76AEADC96D7B (U3CU3Ec_t682782D0E83E1C6EAB4F75B276D4EC4DFEF13B1D* __this, RaycastHit_t6F30BD0B38B56401CA833A1B87BD74F2ACD2F2B5 ___0_r1, RaycastHit_t6F30BD0B38B56401CA833A1B87BD74F2ACD2F2B5 ___1_r2, const RuntimeMethod* method) 
{
	float V_0 = 0.0f;
	{
		// System.Array.Sort(hits, (r1, r2) => r1.distance.CompareTo(r2.distance));
		float L_0;
		L_0 = RaycastHit_get_distance_m035194B0E9BB6229259CFC43B095A9C8E5011C78((&___0_r1), NULL);
		V_0 = L_0;
		float L_1;
		L_1 = RaycastHit_get_distance_m035194B0E9BB6229259CFC43B095A9C8E5011C78((&___1_r2), NULL);
		int32_t L_2;
		L_2 = Single_CompareTo_m06F7868162EB392D3E99103D1A0BD27463C9E66F((&V_0), L_1, NULL);
		return L_2;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void UnityEngine.EventSystems.OVRPointerEventData::.ctor(UnityEngine.EventSystems.EventSystem)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void OVRPointerEventData__ctor_mF3624D599F08C10D0D2E14CB8E8016AEC1FFA1B9 (OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* __this, EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707* ___0_eventSystem, const RuntimeMethod* method) 
{
	{
		// : base(eventSystem)
		EventSystem_t61C51380B105BE9D2C39C4F15B7E655659957707* L_0 = ___0_eventSystem;
		PointerEventData__ctor_m63837790B68893F0022CCEFEF26ADD55A975F71C(__this, L_0, NULL);
		// }
		return;
	}
}
// System.String UnityEngine.EventSystems.OVRPointerEventData::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* OVRPointerEventData_ToString_m37BD8F9E0912E8BD6F215B28D5C662BD60AA5261 (OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringBuilder_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral06A17D93E6D67BEA37EBBF3A9D7FC06B40689CD1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1EC5A90CC65C8BDA58643C956BA4E6F8E4436A5A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1FF1E10A290EC11AB4C6B8CA7BD71BA07C515124);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral709C1BE385364BF7215290F6C80B9E13ED6C07AB);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral97949C809FAE8AD49BF4DA35A46951F99B00E5E1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9C5CA5F3D440697E7CE47F45B0AE3B6DB74C2054);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralAAD329BEE4AA4299DC498EF86EE4D802F5F77951);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB4705CCB6B015DADE9B7063D15E59D6BAE057C37);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBC250738CF6553169DE970EACBEDFB060B58A41B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDB1334B07CE2A0153E77054CF8FA3829A2097735);
		s_Il2CppMethodInitialized = true;
	}
	StringBuilder_t* V_0 = NULL;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	bool V_2 = false;
	Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 V_3;
	memset((&V_3), 0, sizeof(V_3));
	String_t* V_4 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B2_0 = NULL;
	String_t* G_B2_1 = NULL;
	StringBuilder_t* G_B2_2 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B1_0 = NULL;
	String_t* G_B1_1 = NULL;
	StringBuilder_t* G_B1_2 = NULL;
	String_t* G_B3_0 = NULL;
	String_t* G_B3_1 = NULL;
	StringBuilder_t* G_B3_2 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B5_0 = NULL;
	String_t* G_B5_1 = NULL;
	StringBuilder_t* G_B5_2 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B4_0 = NULL;
	String_t* G_B4_1 = NULL;
	StringBuilder_t* G_B4_2 = NULL;
	String_t* G_B6_0 = NULL;
	String_t* G_B6_1 = NULL;
	StringBuilder_t* G_B6_2 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B8_0 = NULL;
	String_t* G_B8_1 = NULL;
	StringBuilder_t* G_B8_2 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B7_0 = NULL;
	String_t* G_B7_1 = NULL;
	StringBuilder_t* G_B7_2 = NULL;
	String_t* G_B9_0 = NULL;
	String_t* G_B9_1 = NULL;
	StringBuilder_t* G_B9_2 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B11_0 = NULL;
	String_t* G_B11_1 = NULL;
	StringBuilder_t* G_B11_2 = NULL;
	GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* G_B10_0 = NULL;
	String_t* G_B10_1 = NULL;
	StringBuilder_t* G_B10_2 = NULL;
	String_t* G_B12_0 = NULL;
	String_t* G_B12_1 = NULL;
	StringBuilder_t* G_B12_2 = NULL;
	{
		// var sb = new StringBuilder();
		StringBuilder_t* L_0 = (StringBuilder_t*)il2cpp_codegen_object_new(StringBuilder_t_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		StringBuilder__ctor_m1D99713357DE05DAFA296633639DB55F8C30587D(L_0, NULL);
		V_0 = L_0;
		// sb.AppendLine("<b>Position</b>: " + position);
		StringBuilder_t* L_1 = V_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_2;
		L_2 = PointerEventData_get_position_m5BE71C28EB72EFB8435749E4E6E839213AEF458C_inline(__this, NULL);
		V_1 = L_2;
		String_t* L_3;
		L_3 = Vector2_ToString_mB47B29ECB21FA3A4ACEABEFA18077A5A6BBCCB27((&V_1), NULL);
		String_t* L_4;
		L_4 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralAAD329BEE4AA4299DC498EF86EE4D802F5F77951, L_3, NULL);
		NullCheck(L_1);
		StringBuilder_t* L_5;
		L_5 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(L_1, L_4, NULL);
		// sb.AppendLine("<b>delta</b>: " + delta);
		StringBuilder_t* L_6 = V_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_7;
		L_7 = PointerEventData_get_delta_m7DC87C01EAE1D10282C37842ED215FDBFE2C1C5B_inline(__this, NULL);
		V_1 = L_7;
		String_t* L_8;
		L_8 = Vector2_ToString_mB47B29ECB21FA3A4ACEABEFA18077A5A6BBCCB27((&V_1), NULL);
		String_t* L_9;
		L_9 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralDB1334B07CE2A0153E77054CF8FA3829A2097735, L_8, NULL);
		NullCheck(L_6);
		StringBuilder_t* L_10;
		L_10 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(L_6, L_9, NULL);
		// sb.AppendLine("<b>eligibleForClick</b>: " + eligibleForClick);
		StringBuilder_t* L_11 = V_0;
		bool L_12;
		L_12 = PointerEventData_get_eligibleForClick_m4B01A1640C694FD7421BDFB19CF763BC85672C8E_inline(__this, NULL);
		V_2 = L_12;
		String_t* L_13;
		L_13 = Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63((&V_2), NULL);
		String_t* L_14;
		L_14 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralB4705CCB6B015DADE9B7063D15E59D6BAE057C37, L_13, NULL);
		NullCheck(L_11);
		StringBuilder_t* L_15;
		L_15 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(L_11, L_14, NULL);
		// sb.AppendLine("<b>pointerEnter</b>: " + pointerEnter);
		StringBuilder_t* L_16 = V_0;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_17;
		L_17 = PointerEventData_get_pointerEnter_m6CE76D5C0C36C4666CDDE348B57885C52D495A4B_inline(__this, NULL);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_18 = L_17;
		G_B1_0 = L_18;
		G_B1_1 = _stringLiteral9C5CA5F3D440697E7CE47F45B0AE3B6DB74C2054;
		G_B1_2 = L_16;
		if (L_18)
		{
			G_B2_0 = L_18;
			G_B2_1 = _stringLiteral9C5CA5F3D440697E7CE47F45B0AE3B6DB74C2054;
			G_B2_2 = L_16;
			goto IL_0083;
		}
	}
	{
		G_B3_0 = ((String_t*)(NULL));
		G_B3_1 = G_B1_1;
		G_B3_2 = G_B1_2;
		goto IL_0088;
	}

IL_0083:
	{
		NullCheck(G_B2_0);
		String_t* L_19;
		L_19 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, G_B2_0);
		G_B3_0 = L_19;
		G_B3_1 = G_B2_1;
		G_B3_2 = G_B2_2;
	}

IL_0088:
	{
		String_t* L_20;
		L_20 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(G_B3_1, G_B3_0, NULL);
		NullCheck(G_B3_2);
		StringBuilder_t* L_21;
		L_21 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(G_B3_2, L_20, NULL);
		// sb.AppendLine("<b>pointerPress</b>: " + pointerPress);
		StringBuilder_t* L_22 = V_0;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_23;
		L_23 = PointerEventData_get_pointerPress_mEE815DDB67E40AA587090BCCE0E3CABA6405C50A(__this, NULL);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_24 = L_23;
		G_B4_0 = L_24;
		G_B4_1 = _stringLiteralBC250738CF6553169DE970EACBEDFB060B58A41B;
		G_B4_2 = L_22;
		if (L_24)
		{
			G_B5_0 = L_24;
			G_B5_1 = _stringLiteralBC250738CF6553169DE970EACBEDFB060B58A41B;
			G_B5_2 = L_22;
			goto IL_00a6;
		}
	}
	{
		G_B6_0 = ((String_t*)(NULL));
		G_B6_1 = G_B4_1;
		G_B6_2 = G_B4_2;
		goto IL_00ab;
	}

IL_00a6:
	{
		NullCheck(G_B5_0);
		String_t* L_25;
		L_25 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, G_B5_0);
		G_B6_0 = L_25;
		G_B6_1 = G_B5_1;
		G_B6_2 = G_B5_2;
	}

IL_00ab:
	{
		String_t* L_26;
		L_26 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(G_B6_1, G_B6_0, NULL);
		NullCheck(G_B6_2);
		StringBuilder_t* L_27;
		L_27 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(G_B6_2, L_26, NULL);
		// sb.AppendLine("<b>lastPointerPress</b>: " + lastPress);
		StringBuilder_t* L_28 = V_0;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_29;
		L_29 = PointerEventData_get_lastPress_m46720C62503214A44EE947679A8BA307BC2AEEDC_inline(__this, NULL);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_30 = L_29;
		G_B7_0 = L_30;
		G_B7_1 = _stringLiteral1EC5A90CC65C8BDA58643C956BA4E6F8E4436A5A;
		G_B7_2 = L_28;
		if (L_30)
		{
			G_B8_0 = L_30;
			G_B8_1 = _stringLiteral1EC5A90CC65C8BDA58643C956BA4E6F8E4436A5A;
			G_B8_2 = L_28;
			goto IL_00c9;
		}
	}
	{
		G_B9_0 = ((String_t*)(NULL));
		G_B9_1 = G_B7_1;
		G_B9_2 = G_B7_2;
		goto IL_00ce;
	}

IL_00c9:
	{
		NullCheck(G_B8_0);
		String_t* L_31;
		L_31 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, G_B8_0);
		G_B9_0 = L_31;
		G_B9_1 = G_B8_1;
		G_B9_2 = G_B8_2;
	}

IL_00ce:
	{
		String_t* L_32;
		L_32 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(G_B9_1, G_B9_0, NULL);
		NullCheck(G_B9_2);
		StringBuilder_t* L_33;
		L_33 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(G_B9_2, L_32, NULL);
		// sb.AppendLine("<b>pointerDrag</b>: " + pointerDrag);
		StringBuilder_t* L_34 = V_0;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_35;
		L_35 = PointerEventData_get_pointerDrag_m36BF08A32216845A8095C5F74DFE6C9959A11E96_inline(__this, NULL);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_36 = L_35;
		G_B10_0 = L_36;
		G_B10_1 = _stringLiteral06A17D93E6D67BEA37EBBF3A9D7FC06B40689CD1;
		G_B10_2 = L_34;
		if (L_36)
		{
			G_B11_0 = L_36;
			G_B11_1 = _stringLiteral06A17D93E6D67BEA37EBBF3A9D7FC06B40689CD1;
			G_B11_2 = L_34;
			goto IL_00ec;
		}
	}
	{
		G_B12_0 = ((String_t*)(NULL));
		G_B12_1 = G_B10_1;
		G_B12_2 = G_B10_2;
		goto IL_00f1;
	}

IL_00ec:
	{
		NullCheck(G_B11_0);
		String_t* L_37;
		L_37 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, G_B11_0);
		G_B12_0 = L_37;
		G_B12_1 = G_B11_1;
		G_B12_2 = G_B11_2;
	}

IL_00f1:
	{
		String_t* L_38;
		L_38 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(G_B12_1, G_B12_0, NULL);
		NullCheck(G_B12_2);
		StringBuilder_t* L_39;
		L_39 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(G_B12_2, L_38, NULL);
		// sb.AppendLine("<b>worldSpaceRay</b>: " + worldSpaceRay);
		StringBuilder_t* L_40 = V_0;
		Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 L_41 = __this->___worldSpaceRay_33;
		V_3 = L_41;
		String_t* L_42;
		L_42 = Ray_ToString_m06274331D92120539B4C6E0D3747EE620DB468E5((&V_3), NULL);
		String_t* L_43;
		L_43 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral1FF1E10A290EC11AB4C6B8CA7BD71BA07C515124, L_42, NULL);
		NullCheck(L_40);
		StringBuilder_t* L_44;
		L_44 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(L_40, L_43, NULL);
		// sb.AppendLine("<b>swipeStart</b>: " + swipeStart);
		StringBuilder_t* L_45 = V_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_46 = __this->___swipeStart_34;
		V_1 = L_46;
		String_t* L_47;
		L_47 = Vector2_ToString_mB47B29ECB21FA3A4ACEABEFA18077A5A6BBCCB27((&V_1), NULL);
		String_t* L_48;
		L_48 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral97949C809FAE8AD49BF4DA35A46951F99B00E5E1, L_47, NULL);
		NullCheck(L_45);
		StringBuilder_t* L_49;
		L_49 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(L_45, L_48, NULL);
		// sb.AppendLine("<b>Use Drag Threshold</b>: " + useDragThreshold);
		StringBuilder_t* L_50 = V_0;
		bool L_51;
		L_51 = PointerEventData_get_useDragThreshold_m3ED1F39E71630C9AB1F340C92F8FA39AA489E1C5_inline(__this, NULL);
		V_2 = L_51;
		String_t* L_52;
		L_52 = Boolean_ToString_m6646C8026B1DF381A1EE8CD13549175E9703CC63((&V_2), NULL);
		String_t* L_53;
		L_53 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral709C1BE385364BF7215290F6C80B9E13ED6C07AB, L_52, NULL);
		NullCheck(L_50);
		StringBuilder_t* L_54;
		L_54 = StringBuilder_AppendLine_mF75744CE941C63E33188E22E936B71A24D3CBF88(L_50, L_53, NULL);
		// return sb.ToString();
		StringBuilder_t* L_55 = V_0;
		NullCheck(L_55);
		String_t* L_56;
		L_56 = VirtualFuncInvoker0< String_t* >::Invoke(3 /* System.String System.Object::ToString() */, L_55);
		V_4 = L_56;
		goto IL_016f;
	}

IL_016f:
	{
		// }
		String_t* L_57 = V_4;
		return L_57;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Boolean UnityEngine.EventSystems.PointerEventDataExtension::IsVRPointer(UnityEngine.EventSystems.PointerEventData)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool PointerEventDataExtension_IsVRPointer_m630294B1887266CFC4779146B4D066B6EBF4DCD5 (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* ___0_pointerEventData, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		// return (pointerEventData is OVRPointerEventData);
		PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* L_0 = ___0_pointerEventData;
		V_0 = (bool)((!(((RuntimeObject*)(OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D*)((OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D*)IsInstClass((RuntimeObject*)L_0, OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var))) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
		goto IL_000d;
	}

IL_000d:
	{
		// }
		bool L_1 = V_0;
		return L_1;
	}
}
// UnityEngine.Ray UnityEngine.EventSystems.PointerEventDataExtension::GetRay(UnityEngine.EventSystems.PointerEventData)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 PointerEventDataExtension_GetRay_m8C036451341B0C37A138E9D3BB509B9DFDCADD99 (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* ___0_pointerEventData, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Assert_tDC16963451AC4364803739B73A4477ADCB365863_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* V_0 = NULL;
	Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// OVRPointerEventData vrPointerEventData = pointerEventData as OVRPointerEventData;
		PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* L_0 = ___0_pointerEventData;
		V_0 = ((OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D*)IsInstClass((RuntimeObject*)L_0, OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var));
		// Assert.IsNotNull(vrPointerEventData);
		OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* L_1 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Assert_tDC16963451AC4364803739B73A4477ADCB365863_il2cpp_TypeInfo_var);
		Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590(L_1, Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590_RuntimeMethod_var);
		// return vrPointerEventData.worldSpaceRay;
		OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* L_2 = V_0;
		NullCheck(L_2);
		Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 L_3 = L_2->___worldSpaceRay_33;
		V_1 = L_3;
		goto IL_0018;
	}

IL_0018:
	{
		// }
		Ray_t2B1742D7958DC05BDC3EFC7461D3593E1430DC00 L_4 = V_1;
		return L_4;
	}
}
// UnityEngine.Vector2 UnityEngine.EventSystems.PointerEventDataExtension::GetSwipeStart(UnityEngine.EventSystems.PointerEventData)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 PointerEventDataExtension_GetSwipeStart_m09ED39DC9E87EDFB80926815E29A3B130E3CA586 (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* ___0_pointerEventData, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Assert_tDC16963451AC4364803739B73A4477ADCB365863_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* V_0 = NULL;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		// OVRPointerEventData vrPointerEventData = pointerEventData as OVRPointerEventData;
		PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* L_0 = ___0_pointerEventData;
		V_0 = ((OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D*)IsInstClass((RuntimeObject*)L_0, OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var));
		// Assert.IsNotNull(vrPointerEventData);
		OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* L_1 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Assert_tDC16963451AC4364803739B73A4477ADCB365863_il2cpp_TypeInfo_var);
		Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590(L_1, Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590_RuntimeMethod_var);
		// return vrPointerEventData.swipeStart;
		OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* L_2 = V_0;
		NullCheck(L_2);
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_3 = L_2->___swipeStart_34;
		V_1 = L_3;
		goto IL_0018;
	}

IL_0018:
	{
		// }
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_4 = V_1;
		return L_4;
	}
}
// System.Void UnityEngine.EventSystems.PointerEventDataExtension::SetSwipeStart(UnityEngine.EventSystems.PointerEventData,UnityEngine.Vector2)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PointerEventDataExtension_SetSwipeStart_m3FD15231D3C0B9BC95B84E7048FF8289CF4359C5 (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* ___0_pointerEventData, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___1_start, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Assert_tDC16963451AC4364803739B73A4477ADCB365863_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* V_0 = NULL;
	{
		// OVRPointerEventData vrPointerEventData = pointerEventData as OVRPointerEventData;
		PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* L_0 = ___0_pointerEventData;
		V_0 = ((OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D*)IsInstClass((RuntimeObject*)L_0, OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_il2cpp_TypeInfo_var));
		// Assert.IsNotNull(vrPointerEventData);
		OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* L_1 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Assert_tDC16963451AC4364803739B73A4477ADCB365863_il2cpp_TypeInfo_var);
		Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590(L_1, Assert_IsNotNull_TisOVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D_mB058B605A1DAC76CD12D7D26B4436BF8B7C63590_RuntimeMethod_var);
		// vrPointerEventData.swipeStart = start;
		OVRPointerEventData_t16F6545720F0956B5AAA7A38FA81CF02E7F37B6D* L_2 = V_0;
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_3 = ___1_start;
		NullCheck(L_2);
		L_2->___swipeStart_34 = L_3;
		// }
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 PointerEventData_get_position_m5BE71C28EB72EFB8435749E4E6E839213AEF458C_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) 
{
	{
		// public Vector2 position { get; set; }
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_0 = __this->___U3CpositionU3Ek__BackingField_13;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 PointerEventData_get_delta_m7DC87C01EAE1D10282C37842ED215FDBFE2C1C5B_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) 
{
	{
		// public Vector2 delta { get; set; }
		Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 L_0 = __this->___U3CdeltaU3Ek__BackingField_14;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool PointerEventData_get_eligibleForClick_m4B01A1640C694FD7421BDFB19CF763BC85672C8E_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) 
{
	{
		// public bool eligibleForClick { get; set; }
		bool L_0 = __this->___U3CeligibleForClickU3Ek__BackingField_11;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* PointerEventData_get_pointerEnter_m6CE76D5C0C36C4666CDDE348B57885C52D495A4B_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) 
{
	{
		// public GameObject pointerEnter { get; set; }
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0 = __this->___U3CpointerEnterU3Ek__BackingField_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* PointerEventData_get_lastPress_m46720C62503214A44EE947679A8BA307BC2AEEDC_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) 
{
	{
		// public GameObject lastPress { get; private set; }
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0 = __this->___U3ClastPressU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* PointerEventData_get_pointerDrag_m36BF08A32216845A8095C5F74DFE6C9959A11E96_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) 
{
	{
		// public GameObject pointerDrag { get; set; }
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_0 = __this->___U3CpointerDragU3Ek__BackingField_6;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool PointerEventData_get_useDragThreshold_m3ED1F39E71630C9AB1F340C92F8FA39AA489E1C5_inline (PointerEventData_t9670F3C7D823CCB738A1604C72A1EB90292396FB* __this, const RuntimeMethod* method) 
{
	{
		// public bool useDragThreshold { get; set; }
		bool L_0 = __this->___U3CuseDragThresholdU3Ek__BackingField_21;
		return L_0;
	}
}
