using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTest : MonoBehaviour
{
    ILogger logger;

    void Start()
    {
        this.logger = FileAppender.Create("logfile.txt", true);
    }

    void OnDestroy()
    {
        if (this.logger.logHandler is FileAppender)
            ((FileAppender)this.logger.logHandler).Close();
    }

    void Update()
    {
        // Infoでログを出力
        this.logger.Log("output info!");

        try
        {
            throw new Exception("Exception Fire!!");
        }
        catch (Exception ex)
        {
            // Exception でのログ出力
            this.logger.LogException(ex);
        }
    }
}
