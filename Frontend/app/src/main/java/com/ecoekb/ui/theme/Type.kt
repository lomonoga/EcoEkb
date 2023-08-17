package com.ecoekb.ui.theme

import androidx.compose.material3.Typography
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.sp

val Typography = Typography(
    bodyMedium = TextStyle(
        fontFamily = FontFamily.Default,
        fontWeight = FontWeight.Normal,
        fontSize = 14.sp,
        lineHeight = 16.8.sp,
        letterSpacing = 0.5.sp,
        color = Black,
    ),

    titleMedium = TextStyle(
        fontSize = 28.sp,
        lineHeight = 41.sp,
        letterSpacing = 0.5.sp,
        fontFamily = FontFamily.Default,
        fontWeight = FontWeight.Bold,
        color = Black,
    )
)