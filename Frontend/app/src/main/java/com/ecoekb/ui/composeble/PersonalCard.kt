package com.ecoekb.ui.composeble

import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.painter.ColorPainter
import androidx.compose.ui.unit.dp

@Composable
fun PersonalCard (
    modifier: Modifier = Modifier
) {
    Box(
        modifier = modifier
            .background(MaterialTheme.colorScheme.onPrimary)
    ){
        Row(
            modifier = Modifier.padding(16.dp)
        ){

            Image(
                painter = ColorPainter(Color.Red),
                contentDescription = "Screen1",
                modifier = Modifier.size(100.dp, 100.dp).clip(RoundedCornerShape(5.dp)),
//            contentScale = ContentScale.FillHeight
            )

            Column(
                modifier = Modifier.padding(start = 8.dp)
            ) {
                Text(
                    text = "Анна Иванова",
                    style = MaterialTheme.typography.headlineMedium,
                    modifier = Modifier.padding(top = 8.dp)
                )

                Text(
                    text = "annaivanova@gmail.com",
                    style = MaterialTheme.typography.bodyMedium
                )
            }
        }
    }
}